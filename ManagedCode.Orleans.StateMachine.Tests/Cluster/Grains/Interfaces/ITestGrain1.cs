namespace ManagedCode.Orleans.StateMachine.Tests.Cluster.Grains.Interfaces;

// private state machine
// TODO: grain that implements state machine grain
public interface ITestGrain1 : IGrainWithStringKey
{
    Task<string> Do();
    Task<string> Go();
    Task<string> Take();
}