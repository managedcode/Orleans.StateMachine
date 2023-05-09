using FluentAssertions;
using ManagedCode.Orleans.StateMachine.Tests.Cluster;
using ManagedCode.Orleans.StateMachine.Tests.Cluster.Grains.Interfaces;
using Xunit;
using Xunit.Abstractions;

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
        Assert.ThrowsAsync<InvalidOperationException>(
            () => grain.Do('x'));
    }

    [Fact]
    public async Task TestStatelessGrainTests()
    {
        var grain = _testApp.Cluster.Client.GetGrain<ITestStatelessGrain>("test");

        await grain.FireAsync(' ');
        (await grain.GetStateAsync()).Should().Be(Constants.On);

        await grain.FireAsync(' ');
        (await grain.GetStateAsync()).Should().Be(Constants.Off);

        //No valid leaving transitions are permitted from state 'Off' for trigger 'x'. Consider ignoring the trigger.
        Assert.ThrowsAsync<InvalidOperationException>(
            () => grain.FireAsync('x'));

        var into = await grain.GetInfoAsync();
        into.InitialState.UnderlyingState.Should().Be(Constants.Off);
    }
}