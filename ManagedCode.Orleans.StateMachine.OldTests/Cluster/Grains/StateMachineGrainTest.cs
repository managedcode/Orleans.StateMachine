using ManagedCode.Orleans.StateMachine.Tests.Cluster.Grains.Interfaces;

namespace ManagedCode.Orleans.StateMachine.Tests.Cluster.Grains;

public class StateMachineGrainTest : Grain, IStateMachineGrainTest
{
    public async Task<string> Do()
    {
        await Task.Delay(TimeSpan.FromSeconds(5));
        return "Do";
    }

    public async Task<string> Go()
    {
        await Task.Delay(TimeSpan.FromSeconds(5));
        return "Go";
    }

    public async Task<string> Take()
    {
        await Task.Delay(TimeSpan.FromSeconds(5));
        return "Take";
    }
}