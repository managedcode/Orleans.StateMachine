using Stateless;
using Stateless.Reflection;
using System;
using System.Threading.Tasks;

namespace ManagedCode.Orleans.StateMachine.Interfaces;

public interface IStateGrain<TState, TEvent>
{
    Task<StateGrain<TState, TEvent>> OnEntryOrleansAsync(Func<Task> OnEntryOrleans);
    Task<StateGrain<TState, TEvent>> ConfigureAsync(TState state);
    Task<StateGrain<TState, TEvent>> FireAsync(TEvent eventTrigger);
    Task<StateGrain<TState, TEvent>> PermitAsync(TEvent trigger, TState destinationState);
    Task<StateGrain<TState, TEvent>> PermitIfAsync(TEvent trigger, TState destinationState, Func<bool> guard, string guardDescription = null);
    Task<StateGrain<TState, TEvent>> InternalTransitionIf(TEvent trigger, Func<object[], bool> guard, Action<StateMachine<TState, TEvent>.Transition> entryAction, string guardDescription = null);
    Task<StateGrain<TState, TEvent>> InternalTransitionAsync(TEvent trigger, Action<StateMachine<TState, TEvent>.Transition> entryAction);
    Task<StateGrain<TState, TEvent>> OnEntryAsync(Func<Task> entryAction);
    Task<StateGrain<TState, TEvent>> OnEntryAsync(Action entryAction, string entryActionDescription = null);
    Task<StateGrain<TState, TEvent>> OnEntryAsync(Action<StateMachine<TState, TEvent>.Transition> entryAction, string entryActionDescription = null);
    Task<StateGrain<TState, TEvent>> OnEntryFromAsync(TEvent trigger, Func<Task> entryAction);
    Task<StateGrain<TState, TEvent>> OnEntryFromAsync(TEvent trigger, Action entryAction, string entryActionDescription = null);
    Task<StateGrain<TState, TEvent>> OnEntryFromAsync(TEvent trigger, Action<StateMachine<TState, TEvent>.Transition> entryAction, string entryActionDescription = null);
    Task<StateGrain<TState, TEvent>> OnExitAsync(Func<Task> exitAction);
    Task<StateGrain<TState, TEvent>> OnExitAsync(Action exitAction, string exitActionDescription = null);
    Task<StateGrain<TState, TEvent>> OnExitAsync(Action<StateMachine<TState, TEvent>.Transition> exitAction, string exitActionDescription = null);
    Task<StateGrain<TState, TEvent>> SubstateOfAsync(TState superstate);
    Task<StateGrain<TState, TEvent>> PermitReentryAsync(TEvent trigger);
    Task<StateGrain<TState, TEvent>> PermitReentryIfAsync(TEvent trigger, Func<bool> guard, string guardDescription = null);
    Task<StateGrain<TState, TEvent>> PermitDynamicAsync(TEvent trigger, Func<TState> destinationStateSelector, string destinationStateSelectorDescription = null, DynamicStateInfos possibleDestinationStates = null);
    Task<StateGrain<TState, TEvent>> PermitDynamicIfAsync(TEvent trigger, Func<TState> destinationStateSelector,
                Func<bool> guard, string guardDescription = null, Stateless.Reflection.DynamicStateInfos possibleDestinationStates = null);
    Task<StateGrain<TState, TEvent>> PermitDynamicIfAsync(TEvent trigger, Func<TState> destinationStateSelector,
               string destinationStateSelectorDescription, Func<bool> guard, string guardDescription = null, Stateless.Reflection.DynamicStateInfos possibleDestinationStates = null);
    Task<StateGrain<TState, TEvent>> IgnoreAsync(TEvent trigger);
    Task<StateGrain<TState, TEvent>> IgnoreIfAsync(TEvent trigger, Func<bool> guard, string guardDescription = null);
    Task<StateGrain<TState, TEvent>> OnActivateAsync(Action activateAction, string activateActionDescription = null);
    Task<StateGrain<TState, TEvent>> OnDeactivateAsync(Action deactivateAction, string deactivateActionDescription = null);
    Task<StateGrain<TState, TEvent>> SubstateOf(TState superstate);
    Task<StateGrain<TState, TEvent>> InitialTransitionAsync(TState targetState);
}
