using ManagedCode.Orleans.StateMachine.Tests.Cluster;
using ManagedCode.Orleans.StateMachine.Tests.Cluster.Grains.Interfaces;
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
    public async Task GrainIdTests()
    {
        await _testApp.Cluster.Client.GetGrain<ITestGrain1>("test").Do();
    }
}