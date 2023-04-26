using System;
using System.Threading;
using System.Threading.Tasks;
using ManagedCode.Orleans.StateMachine.Extensions;
using ManagedCode.Orleans.StateMachine.Interfaces;
using Orleans;
using Stateless;
using Stateless.Reflection;

namespace ManagedCode.Orleans.StateMachine;

public abstract class StateMachineGrain<TState, TEvent> : Grain, IStateMachineGrain<TState, TEvent>
{
    protected readonly StateMachine<TState, TEvent>.StateConfiguration _stateConfiguration;
    protected StateMachine<TState, TEvent> StateMachine { get; private set; }

    protected abstract StateMachine<TState, TEvent> BuildStateMachine();

    public override Task OnActivateAsync(CancellationToken cancellationToken)
    {
        StateMachine = BuildStateMachine();
        
        return base.OnActivateAsync(cancellationToken);
    }

    public Task<StateMachineGrain<TState, TEvent>> OnEntryOrleansAsync(Func<Task> onEntryOrleans)
    {
        _stateConfiguration.OnEntryOrleansAsync(onEntryOrleans);

        return Task.FromResult(this);
    }

    public Task<StateMachineGrain<TState, TEvent>> ConfigureAsync(TState state)
    {
        StateMachine.Configure(state);

        return Task.FromResult(this);
    }

    public Task<StateMachineGrain<TState, TEvent>> FireAsync(TEvent eventTrigger)
    {
        StateMachine.Fire(eventTrigger);

        return Task.FromResult(this);
    }

    public Task<StateMachineGrain<TState, TEvent>> PermitAsync(TEvent trigger, TState destinationState)
    {
        _stateConfiguration.Permit(trigger, destinationState);

        return Task.FromResult(this);
    }

    public Task<StateMachineGrain<TState, TEvent>> PermitIfAsync(TEvent trigger, TState destinationState,
        Func<bool> guard, string guardDescription = null)
    {
        _stateConfiguration.PermitIf(trigger, destinationState, guard, guardDescription);

        return Task.FromResult(this);
    }

    public Task<StateMachineGrain<TState, TEvent>> InternalTransitionIf(TEvent trigger, Func<object[], bool> guard,
        Action<StateMachine<TState, TEvent>.Transition> entryAction, string guardDescription = null)
    {
        _stateConfiguration.InternalTransitionIf(trigger, guard, entryAction, guardDescription);

        return Task.FromResult(this);
    }

    public Task<StateMachineGrain<TState, TEvent>> InternalTransitionAsync(TEvent trigger,
        Action<StateMachine<TState, TEvent>.Transition> entryAction)
    {
        _stateConfiguration.InternalTransition(trigger, entryAction);

        return Task.FromResult(this);
    }


    public Task<StateMachineGrain<TState, TEvent>> OnEntryAsync(Func<Task> entryAction)
    {
        _stateConfiguration.OnEntryAsync(entryAction);

        return Task.FromResult(this);
    }

    public Task<StateMachineGrain<TState, TEvent>> OnEntryAsync(Action entryAction,
        string entryActionDescription = null)
    {
        _stateConfiguration.OnEntry(entryAction, entryActionDescription);

        return Task.FromResult(this);
    }

    public Task<StateMachineGrain<TState, TEvent>> OnEntryAsync(
        Action<StateMachine<TState, TEvent>.Transition> entryAction, string entryActionDescription = null)
    {
        _stateConfiguration.OnEntry(entryAction, entryActionDescription);

        return Task.FromResult(this);
    }

    public Task<StateMachineGrain<TState, TEvent>> OnEntryFromAsync(TEvent trigger, Func<Task> entryAction)
    {
        _stateConfiguration.OnEntryFromAsync(trigger, entryAction);

        return Task.FromResult(this);
    }

    public Task<StateMachineGrain<TState, TEvent>> OnEntryFromAsync(TEvent trigger, Action entryAction,
        string entryActionDescription = null)
    {
        _stateConfiguration.OnEntryFrom(trigger, entryAction, entryActionDescription);

        return Task.FromResult(this);
    }

    public Task<StateMachineGrain<TState, TEvent>> OnEntryFromAsync(TEvent trigger,
        Action<StateMachine<TState, TEvent>.Transition> entryAction, string entryActionDescription = null)
    {
        _stateConfiguration.OnEntryFrom(trigger, entryAction, entryActionDescription);

        return Task.FromResult(this);
    }

    public Task<StateMachineGrain<TState, TEvent>> OnExitAsync(Func<Task> exitAction)
    {
        _stateConfiguration.OnExitAsync(exitAction);

        return Task.FromResult(this);
    }

    public Task<StateMachineGrain<TState, TEvent>> OnExitAsync(Action exitAction, string exitActionDescription = null)
    {
        _stateConfiguration.OnExit(exitAction, exitActionDescription);

        return Task.FromResult(this);
    }

    public Task<StateMachineGrain<TState, TEvent>> OnExitAsync(
        Action<StateMachine<TState, TEvent>.Transition> exitAction, string exitActionDescription = null)
    {
        _stateConfiguration.OnExit(exitAction, exitActionDescription);

        return Task.FromResult(this);
    }

    public Task<StateMachineGrain<TState, TEvent>> SubstateOfAsync(TState superstate)
    {
        _stateConfiguration.SubstateOf(superstate);

        return Task.FromResult(this);
    }

    public Task<StateMachineGrain<TState, TEvent>> PermitReentryAsync(TEvent trigger)
    {
        _stateConfiguration.PermitReentry(trigger);

        return Task.FromResult(this);
    }

    public Task<StateMachineGrain<TState, TEvent>> PermitReentryIfAsync(TEvent trigger, Func<bool> guard,
        string guardDescription = null)
    {
        _stateConfiguration.PermitReentryIf(trigger, guard, guardDescription);

        return Task.FromResult(this);
    }

    public Task<StateMachineGrain<TState, TEvent>> PermitDynamicAsync(TEvent trigger,
        Func<TState> destinationStateSelector, string destinationStateSelectorDescription = null,
        DynamicStateInfos possibleDestinationStates = null)
    {
        _stateConfiguration.PermitDynamic(trigger, destinationStateSelector, destinationStateSelectorDescription,
            possibleDestinationStates);

        return Task.FromResult(this);
    }

    public Task<StateMachineGrain<TState, TEvent>> PermitDynamicIfAsync(TEvent trigger,
        Func<TState> destinationStateSelector,
        Func<bool> guard, string guardDescription = null, DynamicStateInfos possibleDestinationStates = null)
    {
        _stateConfiguration.PermitDynamicIf(trigger, destinationStateSelector, guard, guardDescription,
            possibleDestinationStates);

        return Task.FromResult(this);
    }

    public Task<StateMachineGrain<TState, TEvent>> PermitDynamicIfAsync(TEvent trigger,
        Func<TState> destinationStateSelector,
        string destinationStateSelectorDescription, Func<bool> guard, string guardDescription = null,
        DynamicStateInfos possibleDestinationStates = null)
    {
        _stateConfiguration.PermitDynamicIf(trigger, destinationStateSelector, destinationStateSelectorDescription,
            guard, guardDescription, possibleDestinationStates);

        return Task.FromResult(this);
    }


    public Task<StateMachineGrain<TState, TEvent>> IgnoreAsync(TEvent trigger)
    {
        _stateConfiguration.Ignore(trigger);

        return Task.FromResult(this);
    }

    public Task<StateMachineGrain<TState, TEvent>> IgnoreIfAsync(TEvent trigger, Func<bool> guard,
        string guardDescription = null)
    {
        _stateConfiguration.IgnoreIf(trigger, guard, guardDescription);

        return Task.FromResult(this);
    }

    public Task<StateMachineGrain<TState, TEvent>> OnActivateAsync(Action activateAction,
        string activateActionDescription = null)
    {
        _stateConfiguration.OnActivate(activateAction, activateActionDescription);

        return Task.FromResult(this);
    }

    public Task<StateMachineGrain<TState, TEvent>> OnDeactivateAsync(Action deactivateAction,
        string deactivateActionDescription = null)
    {
        _stateConfiguration.OnDeactivate(deactivateAction, deactivateActionDescription);

        return Task.FromResult(this);
    }

    public Task<StateMachineGrain<TState, TEvent>> SubstateOf(TState superstate)
    {
        _stateConfiguration.SubstateOf(superstate);

        return Task.FromResult(this);
    }

    public Task<StateMachineGrain<TState, TEvent>> InitialTransitionAsync(TState targetState)
    {
        _stateConfiguration.InitialTransition(targetState);

        return Task.FromResult(this);
    }
}