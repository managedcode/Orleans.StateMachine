using ManagedCode.Orleans.StateMachine.Interfaces;

namespace ManagedCode.Orleans.StateMachine.Tests.Cluster.Grains.Interfaces;


public interface ITestGrain : IGrainWithStringKey
{
    Task<string> Do(char input);
}

public interface ITestStatelessGrain : IGrainWithStringKey, IStateMachineGrain<string, char>
{
    Task<string> DoSomethingElse(char input);
}

public static class Constants
{
    public const string On = "On";
    public const string Off = "Off";
    public const char Space = ' ';
}