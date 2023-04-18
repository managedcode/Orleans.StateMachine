namespace ManagedCode.Orleans.StateMachine.Tests.Cluster.Grains.Interfaces;

public interface IStateMachineGrainTest : IGrainWithStringKey
{
    Task<string> Do();
    Task<string> Go();
    Task<string> Take();
}