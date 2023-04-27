using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ManagedCode.Orleans.StateMachine.Models.Surrogates;
using Stateless;
using Stateless.Graph;
using Stateless.Reflection;

namespace ManagedCode.Orleans.StateMachine.Interfaces;

public interface IStateMachineGrain<TState, TTrigger>
{

    /// <summary>
    /// Activates current state in asynchronous fashion. Actions associated with activating the currrent state
    /// will be invoked. The activation is idempotent and subsequent activation of the same current state 
    /// will not lead to re-execution of activation callbacks.
    /// </summary>
    Task ActivateAsync();

    /// <summary>
    /// Deactivates current state in asynchronous fashion. Actions associated with deactivating the currrent state
    /// will be invoked. The deactivation is idempotent and subsequent deactivation of the same current state 
    /// will not lead to re-execution of deactivation callbacks.
    /// </summary>
    Task DeactivateAsync();

    /// <summary>
    /// Transition from the current state via the specified trigger in async fashion.
    /// The target state is determined by the configuration of the current state.
    /// Actions associated with leaving the current state and entering the new one
    /// will be invoked.
    /// </summary>
    /// <param name="trigger">The trigger to fire.</param>
    /// <exception cref="System.InvalidOperationException">The current state does
    /// not allow the trigger to be fired.</exception>
    Task FireAsync(TTrigger trigger);

    /// <summary>
    /// Transition from the current state via the specified trigger in async fashion.
    /// The target state is determined by the configuration of the current state.
    /// Actions associated with leaving the current state and entering the new one
    /// will be invoked.
    /// </summary>
    /// <typeparam name="TArg0">Type of the first trigger argument.</typeparam>
    /// <param name="trigger">The trigger to fire.</param>
    /// <param name="arg0">The first argument.</param>
    /// <exception cref="System.InvalidOperationException">The current state does
    /// not allow the trigger to be fired.</exception>
    public Task FireAsync<TArg0>(StateMachine<TState, TTrigger>.TriggerWithParameters<TArg0> trigger, TArg0 arg0);

    /// <summary>
    /// Transition from the current state via the specified trigger in async fashion.
    /// The target state is determined by the configuration of the current state.
    /// Actions associated with leaving the current state and entering the new one
    /// will be invoked.
    /// </summary>
    /// <typeparam name="TArg0">Type of the first trigger argument.</typeparam>
    /// <typeparam name="TArg1">Type of the second trigger argument.</typeparam>
    /// <param name="arg0">The first argument.</param>
    /// <param name="arg1">The second argument.</param>
    /// <param name="trigger">The trigger to fire.</param>
    /// <exception cref="System.InvalidOperationException">The current state does
    /// not allow the trigger to be fired.</exception>
    Task FireAsync<TArg0, TArg1>(StateMachine<TState, TTrigger>.TriggerWithParameters<TArg0, TArg1> trigger, TArg0 arg0, TArg1 arg1);

    /// <summary>
    /// Transition from the current state via the specified trigger in async fashion.
    /// The target state is determined by the configuration of the current state.
    /// Actions associated with leaving the current state and entering the new one
    /// will be invoked.
    /// </summary>
    /// <typeparam name="TArg0">Type of the first trigger argument.</typeparam>
    /// <typeparam name="TArg1">Type of the second trigger argument.</typeparam>
    /// <typeparam name="TArg2">Type of the third trigger argument.</typeparam>
    /// <param name="arg0">The first argument.</param>
    /// <param name="arg1">The second argument.</param>
    /// <param name="arg2">The third argument.</param>
    /// <param name="trigger">The trigger to fire.</param>
    /// <exception cref="System.InvalidOperationException">The current state does
    /// not allow the trigger to be fired.</exception>
    Task FireAsync<TArg0, TArg1, TArg2>(
    StateMachine<TState, TTrigger>.TriggerWithParameters<TArg0, TArg1, TArg2> trigger, TArg0 arg0, TArg1 arg1,
    TArg2 arg2);
    
    /// <summary>
    ///  The current state.
    /// </summary>
    Task<TState> GetStateAsync();
    
    /// <summary>
    /// Determine if the state machine is in the supplied state.
    /// </summary>
    /// <param name="state">The state to test for.</param>
    /// <returns>True if the current state is equal to, or a substate of,
    /// the supplied state.</returns>
    Task<bool> IsInStateAsync(TState state);
    
    /// <summary>
    /// Returns true if <paramref name="trigger" /> can be fired
    /// in the current state.
    /// </summary>
    /// <param name="trigger">Trigger to test.</param>
    /// <returns>True if the trigger can be fired, false otherwise.</returns>
    Task<bool> CanFireAsync(TTrigger trigger);
    
    /// <summary>
    /// Provides an info object which exposes the states, transitions, and actions of this machine.
    /// </summary>
     Task<OrleansStateMachineInfo> GetInfoAsync();

    
    //
    //
    // /// <summary>
    // /// Override the default behaviour of throwing an exception when an unhandled trigger
    // /// is fired.
    // /// </summary>
    // /// <param name="unhandledTriggerAction"></param>
    // Task OnUnhandledTriggerAsync(Func<TState, TTrigger, Task> unhandledTriggerAction);
    //
    // /// <summary>
    // /// Override the default behaviour of throwing an exception when an unhandled trigger
    // /// is fired.
    // /// </summary>
    // /// <param name="unhandledTriggerAction">An asynchronous action to call when an unhandled trigger is fired.</param>
    // Task OnUnhandledTriggerAsync(Func<TState, TTrigger, ICollection<string>, Task> unhandledTriggerAction);
    //
    // /// <summary>
    // /// Registers an asynchronous callback that will be invoked every time the statemachine
    // /// transitions from one state into another.
    // /// </summary>
    // /// <param name="onTransitionAction">The asynchronous action to execute, accepting the details
    // /// of the transition.</param>
    // Task OnTransitionedAsync(Func<Transition, Task> onTransitionAction);
    //
    //
    // /// <summary>
    // /// Registers a callback that will be invoked every time the statemachine
    // /// transitions from one state into another and all the OnEntryFrom etc methods
    // /// have been invoked
    // /// </summary>
    // /// <param name="onTransitionAction">The asynchronous action to execute, accepting the details
    // /// of the transition.</param>
    // Task OnTransitionCompletedAsync(Func<Transition, Task> onTransitionAction);


}