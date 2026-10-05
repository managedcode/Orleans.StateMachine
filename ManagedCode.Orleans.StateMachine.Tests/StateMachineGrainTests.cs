using FluentAssertions;
using ManagedCode.Orleans.StateMachine.Tests.Cluster;
using ManagedCode.Orleans.StateMachine.Tests.Cluster.Grains.Interfaces;
using Xunit;
using Xunit.Abstractions;
using System.Collections.Generic; // Added for List<string>
using Stateless; // Added for StateMachine

namespace ManagedCode.Orleans.StateMachine.Tests;

[Collection(nameof(TestClusterApplication))]
public class StateMachineGrainTests
{
    private readonly ITestOutputHelper _outputHelper;
    private readonly TestClusterApplication _testApp;

    public StateMachineGrainTests(TestClusterApplication testApp, ITestOutputHelper outputHelper)
    {
        _testApp = testApp;
        _outputHelper = outputHelper;
    }

    [Fact]
    public async Task TestGrainTests()
    {
        var grain = _testApp.Cluster.Client.GetGrain<ITestGrain>("test");

        var state = await grain.Do(' ');
        state.Should().Be(Constants.On);

        state = await grain.Do(' ');
        state.Should().Be(Constants.Off);

        //No valid leaving transitions are permitted from state 'Off' for trigger 'x'. Consider ignoring the trigger.
        await Assert.ThrowsAsync<InvalidOperationException>(
            () => grain.Do('x'));
    }

    [Fact]
    public async Task TestStatelessGrainTests()
    {
        var grain = _testApp.Cluster.Client.GetGrain<ITestStatelessGrain>("test-stateless"); // Changed key

        await grain.FireAsync(' ');
        (await grain.GetStateAsync()).Should().Be(Constants.On);

        await grain.FireAsync(' ');
        (await grain.GetStateAsync()).Should().Be(Constants.Off);

        //No valid leaving transitions are permitted from state 'Off' for trigger 'x'. Consider ignoring the trigger.
        await Assert.ThrowsAsync<InvalidOperationException>(
            () => grain.FireAsync('x'));

        var into = await grain.GetInfoAsync();
        into.InitialState.UnderlyingState.Should().Be(Constants.Off);
    }

    [Fact]
    public async Task PermittedTriggersAreReturnedThroughOrleansAsync()
    {
        var grain = _testApp.Cluster.Client.GetGrain<ITestStatelessGrain>("permitted-triggers");

        (await grain.GetPermittedTriggersAsync()).Should().Equal(Constants.Space);
        (await grain.GetPermittedTriggersPropertyAsync()).Should().Equal(Constants.Space);
        (await grain.GetDetailedPermittedTriggersAsync()).Should().ContainSingle();

        await grain.FireAsync(Constants.Space);
        (await grain.GetStateAsync()).Should().Be(Constants.On);
        (await grain.GetPermittedTriggersAsync()).Should().Equal(Constants.Space);
    }

    [Fact]
    public async Task ParameterTypesAreReturnedThroughOrleansAsync()
    {
        var grain = _testApp.Cluster.Client.GetGrain<ITestOrleansContextGrain>("trigger-parameter-metadata");
        await grain.FireAsync(TestOrleansContextTriggers.Activate);
        await grain.FireAsync(TestOrleansContextTriggers.Process, 123);
        await grain.FireAsync(TestOrleansContextTriggers.Reset);

        var details = await grain.GetDetailedPermittedTriggersAsync();
        var process = details.Single(detail => detail.Trigger == TestOrleansContextTriggers.Process);
        process.HasParameters.Should().BeTrue();
        process.ArgumentTypes.Should().Equal(typeof(int));
        (await grain.GetStateAsync()).Should().Be(TestOrleansContextStates.Active);
    }

    [Fact]
    public async Task OrleansContextExtensions_ExecuteInOrder()
    {
        var grain = _testApp.Cluster.Client.GetGrain<ITestOrleansContextGrain>("test-orleans-context");
        await grain.ClearLog();

        // 1. Initial Activation
        await grain.ActivateAsync();
        var log = await grain.GetExecutionLog();
        log.Should().ContainInOrder("Initial.Activate");
        (await grain.GetStateAsync()).Should().Be(TestOrleansContextStates.Initial);
        await grain.ClearLog();

        // 2. Transition Initial -> Active
        await grain.FireAsync(TestOrleansContextTriggers.Activate);
        log = await grain.GetExecutionLog();
        log.Should().ContainInOrder(
            "Initial.Exit", // Expect OnExit during transition
            "Active.Entry",
            "Active.EntryFrom.Activate"
        );
        (await grain.GetStateAsync()).Should().Be(TestOrleansContextStates.Active);
        await grain.ClearLog();

        // 3. Activate Active state (should trigger OnActivate)
        await grain.ActivateAsync();
        log = await grain.GetExecutionLog();
        log.Should().ContainInOrder("Active.Activate");
        await grain.ClearLog();

        // 4. Transition Active -> Processing (with parameters)
        await grain.FireAsync(TestOrleansContextTriggers.Process, 123);
        log = await grain.GetExecutionLog();
        log.Should().ContainInOrder(
            "Active.Exit", // Expect OnExit during transition
            "Processing.Entry (via Process)",
            "Processing.EntryFrom.Process (id:123)"
        );
        (await grain.GetStateAsync()).Should().Be(TestOrleansContextStates.Processing);
        await grain.ClearLog();

        // 5. Transition Processing -> Final (with parameters)
        await grain.FireAsync(TestOrleansContextTriggers.Complete, "Done", true);
        log = await grain.GetExecutionLog();
        log.Should().ContainInOrder(
            "Processing.Exit (to Final)",
            "Final.Entry",
            "Final.EntryFrom.Complete (msg:Done, success:true)"
        );
        (await grain.GetStateAsync()).Should().Be(TestOrleansContextStates.Final);
        await grain.ClearLog();

        // 6. Activate Final state
        await grain.ActivateAsync();
        log = await grain.GetExecutionLog();
        log.Should().ContainInOrder("Final.Activate");
        await grain.ClearLog();

        // 7. Deactivate Final state (should not trigger OnExit as it's not a transition)
        await grain.DeactivateAsync();
        log = await grain.GetExecutionLog();
        log.Should().ContainInOrder("Final.Deactivate");
        await grain.ClearLog();

        // 8. Reset from Processing back to Active (test OnEntryFrom with Transition)
        (await grain.CanFireAsync(TestOrleansContextTriggers.Activate)).Should().BeFalse(); // Go back to Active first
        await grain.ClearLog();

        await grain.FireAsync(TestOrleansContextTriggers.Reset, "Reason", 99, false);
        log = await grain.GetExecutionLog();
        log.Should().ContainInOrder(
             "Final.Exit", // Exit Processing
             "Active.Entry",                // Enter Active
             "Active.EntryFrom.Reset (via Reset)" // Specific EntryFrom for Reset
        );
        (await grain.GetStateAsync()).Should().Be(TestOrleansContextStates.Active);
    }
}
