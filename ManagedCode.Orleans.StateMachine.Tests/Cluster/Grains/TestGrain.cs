using ManagedCode.Orleans.StateMachine.Tests.Cluster.Grains.Interfaces;
using Stateless;

namespace ManagedCode.Orleans.StateMachine.Tests.Cluster.Grains;

public class TestGrain : StateMachineGrain<string, char>, ITestGrain
{
    public Task<string> Do(char input)
    {
        StateMachine.Fire(input);
        return Task.FromResult(StateMachine.State);
    }

    protected override StateMachine<string, char> BuildStateMachine()
    {
        // Instantiate a new state machine in the 'off' state
        var onOffSwitch = new StateMachine<string, char>(Constants.Off);

        // Configure state machine with the Configure method, supplying the state to be configured as a parameter
        onOffSwitch.Configure(Constants.Off).Permit(Constants.Space, Constants.On);
        onOffSwitch.Configure(Constants.On).Permit(Constants.Space, Constants.Off);

        return onOffSwitch;
    }
}