using ManagedCode.Orleans.StateMachine.Stateless;
using ManagedCode.Orleans.StateMachine.Tests.Cluster.Grains.Interfaces;
using Stateless;

namespace ManagedCode.Orleans.StateMachine.Tests.Cluster.Grains;

public class TestGrain : StateMachineGrain<string, char>, ITestGrain
{
    protected override StateMachine<string, char> BuildStateMachine()
    {
        // Instantiate a new state machine in the 'off' state
        var onOffSwitch = new StateMachine<string, char>(Constants.Off);

        // Configure state machine with the Configure method, supplying the state to be configured as a parameter
        onOffSwitch.Configure(Constants.Off).Permit(Constants.Space, Constants.On);
        onOffSwitch.Configure(Constants.On).Permit(Constants.Space, Constants.Off);

        return onOffSwitch;
    }

    public Task<string> Do(char input)
    {
        this.StateMachine.Fire(input);
        return Task.FromResult(this.StateMachine.State);
    }
}

public class TestStatelessGrain : StateMachineGrain<string, char>, ITestStatelessGrain
{
    protected override StateMachine<string, char> BuildStateMachine()
    {
        // Instantiate a new state machine in the 'off' state
        var onOffSwitch = new StateMachine<string, char>(Constants.Off);

        // Configure state machine with the Configure method, supplying the state to be configured as a parameter
        onOffSwitch.Configure(Constants.Off).Permit(Constants.Space, Constants.On);
        onOffSwitch.Configure(Constants.On).Permit(Constants.Space, Constants.Off);

        return onOffSwitch;
    }
    

    public Task<string> DoSomethingElse(char input)
    {
        return Task.FromResult("OK");
    }
}