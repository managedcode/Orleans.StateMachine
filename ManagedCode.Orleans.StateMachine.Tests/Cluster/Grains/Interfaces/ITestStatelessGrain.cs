using ManagedCode.Orleans.StateMachine.Interfaces;

namespace ManagedCode.Orleans.StateMachine.Tests.Cluster.Grains.Interfaces;

public interface ITestStatelessGrain : IGrainWithStringKey, IStateMachineGrain<string, char>
{
    Task<string> DoSomethingElse(char input);
}