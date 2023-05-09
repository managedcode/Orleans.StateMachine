namespace ManagedCode.Orleans.StateMachine.Tests.Cluster.Grains.Interfaces;

public interface ITestGrain : IGrainWithStringKey
{
    Task<string> Do(char input);
}