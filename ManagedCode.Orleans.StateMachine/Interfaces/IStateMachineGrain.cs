using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ManagedCode.Orleans.StateMachine.Models;
using Stateless;

namespace ManagedCode.Orleans.StateMachine.Interfaces;

/// <summary>
/// Represents a state machine grain that manages states and transitions.
/// </summary>
/// <typeparam name="TState">The type representing the states.</typeparam>
/// <typeparam name="TTrigger">The type representing the triggers that cause state transitions.</typeparam>
public interface IStateMachineGrain<TState, TTrigger>
{
    /// <summary>
    ///     Activates current state in asynchronous fashion. Actions associated with activating the currrent state
    ///     will be invoked. The activation is idempotent and subsequent activation of the same current state
    ///     will not lead to re-execution of activation callbacks.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task ActivateAsync();

    /// <summary>
    ///     Deactivates current state in asynchronous fashion. Actions associated with deactivating the currrent state
    ///     will be invoked. The deactivation is idempotent and subsequent deactivation of the same current state
    ///     will not lead to re-execution of deactivation callbacks.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task DeactivateAsync();

    /// <summary>
    ///     Transition from the current state via the specified trigger in async fashion.
    ///     The target state is determined by the configuration of the current state.
    ///     Actions associated with leaving the current state and entering the new one
    ///     will be invoked.
    /// </summary>
    /// <param name="trigger">The trigger to fire.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    /// <exception cref="System.InvalidOperationException">
    ///     The current state does
    ///     not allow the trigger to be fired.
    /// </exception>
    Task FireAsync(TTrigger trigger);

    /// <summary>
    ///     Asynchronously transitions from the current state via the specified trigger with one parameter.
    ///     The target state is determined by the configuration of the current state.
    ///     Actions associated with leaving the current state and entering the new one will be invoked.
    /// </summary>
    /// <typeparam name="TArg0">Type of the first trigger argument.</typeparam>
    /// <param name="trigger">The trigger to fire.</param>
    /// <param name="arg0">The first argument.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    /// <exception cref="System.InvalidOperationException">The current state does not allow the trigger to be fired.</exception>
    Task FireAsync<TArg0>(TTrigger trigger, TArg0 arg0);

    /// <summary>
    ///     Asynchronously transitions from the current state via the specified trigger with two parameters.
    ///     The target state is determined by the configuration of the current state.
    ///     Actions associated with leaving the current state and entering the new one will be invoked.
    /// </summary>
    /// <typeparam name="TArg0">Type of the first trigger argument.</typeparam>
    /// <typeparam name="TArg1">Type of the second trigger argument.</typeparam>
    /// <param name="trigger">The trigger to fire.</param>
    /// <param name="arg0">The first argument.</param>
    /// <param name="arg1">The second argument.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    /// <exception cref="System.InvalidOperationException">The current state does not allow the trigger to be fired.</exception>
    Task FireAsync<TArg0, TArg1>(TTrigger trigger, TArg0 arg0, TArg1 arg1);

    /// <summary>
    ///     Asynchronously transitions from the current state via the specified trigger with three parameters.
    ///     The target state is determined by the configuration of the current state.
    ///     Actions associated with leaving the current state and entering the new one will be invoked.
    /// </summary>
    /// <typeparam name="TArg0">Type of the first trigger argument.</typeparam>
    /// <typeparam name="TArg1">Type of the second trigger argument.</typeparam>
    /// <typeparam name="TArg2">Type of the third trigger argument.</typeparam>
    /// <param name="trigger">The trigger to fire.</param>
    /// <param name="arg0">The first argument.</param>
    /// <param name="arg1">The second argument.</param>
    /// <param name="arg2">The third argument.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    /// <exception cref="System.InvalidOperationException">The current state does not allow the trigger to be fired.</exception>
    Task FireAsync<TArg0, TArg1, TArg2>(TTrigger trigger, TArg0 arg0, TArg1 arg1, TArg2 arg2);

    /// <summary>
    ///     Gets the current state of the state machine asynchronously.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation. The task result contains the current state.</returns>
    Task<TState> GetStateAsync();

    /// <summary>
    ///     Determines if the state machine is currently in the specified state or one of its substates asynchronously.
    /// </summary>
    /// <param name="state">The state to test for.</param>
    /// <returns>
    ///     A task that represents the asynchronous operation. The task result contains true if the current state is equal to, or a substate of,
    ///     the supplied state; otherwise, false.
    /// </returns>
    Task<bool> IsInStateAsync(TState state);

    /// <summary>
    ///     Checks if the specified trigger can be fired in the current state asynchronously.
    /// </summary>
    /// <param name="trigger">The trigger to test.</param>
    /// <returns>
    ///     A task that represents the asynchronous operation. The task result contains true if the trigger can be fired in the current state; otherwise, false.
    /// </returns>
    Task<bool> CanFireAsync(TTrigger trigger);

    /// <summary>
    ///     Gets information about the state machine's configuration (states, transitions, actions) asynchronously.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation. The task result contains an <see cref="OrleansStateMachineInfo"/> object.</returns>
    Task<OrleansStateMachineInfo> GetInfoAsync();

    /// <summary>
    ///     Gets the triggers that are permitted to be fired in the current state, considering the provided arguments, asynchronously.
    /// </summary>
    /// <param name="args">Arguments to be passed to the guard functions.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains an enumerable collection of permitted triggers.</returns>
    Task<IEnumerable<TTrigger>> GetPermittedTriggersAsync(params object[] args);

    /// <summary>
    ///     Gets detailed information about the triggers permitted in the current state, considering the provided arguments, asynchronously.
    /// </summary>
    /// <param name="args">Arguments to be passed to the guard functions.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains an enumerable collection of <see cref="OrleansTriggerDetails{TTrigger}"/>.</returns>
    Task<IEnumerable<OrleansTriggerDetails<TTrigger>>> GetDetailedPermittedTriggersAsync(params object[] args);

    /// <summary>
    ///     Gets the triggers that are permitted to be fired in the current state asynchronously (property-like access).
    /// </summary>
    /// <returns>A task that represents the asynchronous operation. The task result contains an enumerable collection of permitted triggers.</returns>
    Task<IEnumerable<TTrigger>> GetPermittedTriggersPropertyAsync();

    /// <summary>
    ///     Checks if the specified trigger can be fired in the current state asynchronously and returns any unmet guard conditions.
    /// </summary>
    /// <param name="trigger">The trigger to test.</param>
    /// <returns>
    ///     A task that represents the asynchronous operation. The task result is a tuple containing:
    ///     - bool: True if the trigger can be fired, false otherwise.
    ///     - ICollection&lt;string&gt;: A collection of descriptions for guard conditions that were not met (if any).
    /// </returns>
    Task<(bool, ICollection<string>)> CanFireWithUnmetGuardsAsync(TTrigger trigger);

    /// <summary>
    ///     Checks if the specified trigger with one argument can be fired in the current state asynchronously.
    /// </summary>
    /// <typeparam name="TArg0">Type of the first trigger argument.</typeparam>
    /// <param name="trigger">The trigger to test.</param>
    /// <param name="arg0">The first argument.</param>
    /// <returns>
    ///     A task that represents the asynchronous operation. The task result contains true if the trigger can be fired with the given argument; otherwise, false.
    /// </returns>
    Task<bool> CanFireAsync<TArg0>(TTrigger trigger, TArg0 arg0);

    /// <summary>
    ///     Checks if the specified trigger with one argument can be fired in the current state asynchronously and returns any unmet guard conditions.
    /// </summary>
    /// <typeparam name="TArg0">Type of the first trigger argument.</typeparam>
    /// <param name="trigger">The trigger to test.</param>
    /// <param name="arg0">The first argument.</param>
    /// <returns>
    ///     A task that represents the asynchronous operation. The task result is a tuple containing:
    ///     - bool: True if the trigger can be fired, false otherwise.
    ///     - ICollection&lt;string&gt;: A collection of descriptions for guard conditions that were not met (if any).
    /// </returns>
    Task<(bool, ICollection<string>)> CanFireWithUnmetGuardsAsync<TArg0>(TTrigger trigger, TArg0 arg0);

    /// <summary>
    ///     Checks if the specified trigger with two arguments can be fired in the current state asynchronously.
    /// </summary>
    /// <typeparam name="TArg0">Type of the first trigger argument.</typeparam>
    /// <typeparam name="TArg1">Type of the second trigger argument.</typeparam>
    /// <param name="trigger">The trigger to test.</param>
    /// <param name="arg0">The first argument.</param>
    /// <param name="arg1">The second argument.</param>
    /// <returns>
    ///     A task that represents the asynchronous operation. The task result contains true if the trigger can be fired with the given arguments; otherwise, false.
    /// </returns>
    Task<bool> CanFireAsync<TArg0, TArg1>(TTrigger trigger, TArg0 arg0, TArg1 arg1);

    /// <summary>
    ///     Checks if the specified trigger with two arguments can be fired in the current state asynchronously and returns any unmet guard conditions.
    /// </summary>
    /// <typeparam name="TArg0">Type of the first trigger argument.</typeparam>
    /// <typeparam name="TArg1">Type of the second trigger argument.</typeparam>
    /// <param name="trigger">The trigger to test.</param>
    /// <param name="arg0">The first argument.</param>
    /// <param name="arg1">The second argument.</param>
    /// <returns>
    ///     A task that represents the asynchronous operation. The task result is a tuple containing:
    ///     - bool: True if the trigger can be fired, false otherwise.
    ///     - ICollection&lt;string&gt;: A collection of descriptions for guard conditions that were not met (if any).
    /// </returns>
    Task<(bool, ICollection<string>)> CanFireWithUnmetGuardsAsync<TArg0, TArg1>(TTrigger trigger, TArg0 arg0, TArg1 arg1);

    /// <summary>
    ///     Checks if the specified trigger with three arguments can be fired in the current state asynchronously.
    /// </summary>
    /// <typeparam name="TArg0">Type of the first trigger argument.</typeparam>
    /// <typeparam name="TArg1">Type of the second trigger argument.</typeparam>
    /// <typeparam name="TArg2">Type of the third trigger argument.</typeparam>
    /// <param name="trigger">The trigger to test.</param>
    /// <param name="arg0">The first argument.</param>
    /// <param name="arg1">The second argument.</param>
    /// <param name="arg2">The third argument.</param>
    /// <returns>
    ///     A task that represents the asynchronous operation. The task result contains true if the trigger can be fired with the given arguments; otherwise, false.
    /// </returns>
    Task<bool> CanFireAsync<TArg0, TArg1, TArg2>(TTrigger trigger, TArg0 arg0, TArg1 arg1, TArg2 arg2);

    /// <summary>
    ///     Checks if the specified trigger with three arguments can be fired in the current state asynchronously and returns any unmet guard conditions.
    /// </summary>
    /// <typeparam name="TArg0">Type of the first trigger argument.</typeparam>
    /// <typeparam name="TArg1">Type of the second trigger argument.</typeparam>
    /// <typeparam name="TArg2">Type of the third trigger argument.</typeparam>
    /// <param name="trigger">The trigger to test.</param>
    /// <param name="arg0">The first argument.</param>
    /// <param name="arg1">The second argument.</param>
    /// <param name="arg2">The third argument.</param>
    /// <returns>
    ///     A task that represents the asynchronous operation. The task result is a tuple containing:
    ///     - bool: True if the trigger can be fired, false otherwise.
    ///     - ICollection&lt;string&gt;: A collection of descriptions for guard conditions that were not met (if any).
    /// </returns>
    Task<(bool, ICollection<string>)> CanFireWithUnmetGuardsAsync<TArg0, TArg1, TArg2>(TTrigger trigger, TArg0 arg0, TArg1 arg1, TArg2 arg2);

    /// <summary>
    ///     Returns a string representation of the state machine's current state and configuration asynchronously.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation. The task result contains a string describing the state machine.</returns>
    Task<string> ToStringAsync();
}
