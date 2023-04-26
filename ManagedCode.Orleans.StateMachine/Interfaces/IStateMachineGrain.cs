using System;
using System.Threading.Tasks;
using Stateless;
using Stateless.Reflection;

namespace ManagedCode.Orleans.StateMachine.Interfaces;

public interface IStateMachineGrain<TState, TEvent>
{
    Task<StateMachineGrain<TState, TEvent>> OnEntryOrleansAsync(Func<Task> onEntryOrleans);
    Task<StateMachineGrain<TState, TEvent>> ConfigureAsync(TState state);
    Task<StateMachineGrain<TState, TEvent>> FireAsync(TEvent eventTrigger);
    Task<StateMachineGrain<TState, TEvent>> PermitAsync(TEvent trigger, TState destinationState);

    Task<StateMachineGrain<TState, TEvent>> PermitIfAsync(TEvent trigger, TState destinationState, Func<bool> guard,
        string guardDescription = null);

    Task<StateMachineGrain<TState, TEvent>> InternalTransitionIf(TEvent trigger, Func<object[], bool> guard,
        Action<StateMachine<TState, TEvent>.Transition> entryAction, string guardDescription = null);

    Task<StateMachineGrain<TState, TEvent>> InternalTransitionAsync(TEvent trigger,
        Action<StateMachine<TState, TEvent>.Transition> entryAction);

    Task<StateMachineGrain<TState, TEvent>> OnEntryAsync(Func<Task> entryAction);
    Task<StateMachineGrain<TState, TEvent>> OnEntryAsync(Action entryAction, string entryActionDescription = null);

    Task<StateMachineGrain<TState, TEvent>> OnEntryAsync(Action<StateMachine<TState, TEvent>.Transition> entryAction,
        string entryActionDescription = null);

    Task<StateMachineGrain<TState, TEvent>> OnEntryFromAsync(TEvent trigger, Func<Task> entryAction);

    Task<StateMachineGrain<TState, TEvent>> OnEntryFromAsync(TEvent trigger, Action entryAction,
        string entryActionDescription = null);

    Task<StateMachineGrain<TState, TEvent>> OnEntryFromAsync(TEvent trigger,
        Action<StateMachine<TState, TEvent>.Transition> entryAction, string entryActionDescription = null);

    Task<StateMachineGrain<TState, TEvent>> OnExitAsync(Func<Task> exitAction);
    Task<StateMachineGrain<TState, TEvent>> OnExitAsync(Action exitAction, string exitActionDescription = null);

    Task<StateMachineGrain<TState, TEvent>> OnExitAsync(Action<StateMachine<TState, TEvent>.Transition> exitAction,
        string exitActionDescription = null);

    Task<StateMachineGrain<TState, TEvent>> SubstateOfAsync(TState superstate);
    Task<StateMachineGrain<TState, TEvent>> PermitReentryAsync(TEvent trigger);

    Task<StateMachineGrain<TState, TEvent>> PermitReentryIfAsync(TEvent trigger, Func<bool> guard,
        string guardDescription = null);

    Task<StateMachineGrain<TState, TEvent>> PermitDynamicAsync(TEvent trigger, Func<TState> destinationStateSelector,
        string destinationStateSelectorDescription = null, DynamicStateInfos possibleDestinationStates = null);

    Task<StateMachineGrain<TState, TEvent>> PermitDynamicIfAsync(TEvent trigger, Func<TState> destinationStateSelector,
        Func<bool> guard, string guardDescription = null, DynamicStateInfos possibleDestinationStates = null);

    Task<StateMachineGrain<TState, TEvent>> PermitDynamicIfAsync(TEvent trigger, Func<TState> destinationStateSelector,
        string destinationStateSelectorDescription, Func<bool> guard, string guardDescription = null,
        DynamicStateInfos possibleDestinationStates = null);

    Task<StateMachineGrain<TState, TEvent>> IgnoreAsync(TEvent trigger);

    Task<StateMachineGrain<TState, TEvent>> IgnoreIfAsync(TEvent trigger, Func<bool> guard,
        string guardDescription = null);

    Task<StateMachineGrain<TState, TEvent>> OnActivateAsync(Action activateAction,
        string activateActionDescription = null);

    Task<StateMachineGrain<TState, TEvent>> OnDeactivateAsync(Action deactivateAction,
        string deactivateActionDescription = null);

    Task<StateMachineGrain<TState, TEvent>> SubstateOf(TState superstate);
    Task<StateMachineGrain<TState, TEvent>> InitialTransitionAsync(TState targetState);
}