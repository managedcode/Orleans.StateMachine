using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using ManagedCode.Orleans.StateMachine.Interfaces;
using ManagedCode.Orleans.StateMachine.Models;
using Orleans;
using Stateless;
using Stateless.Graph;

namespace ManagedCode.Orleans.StateMachine;

/// <summary>
/// Base grain class for integrating a Stateless state machine with Orleans.
/// Provides methods for firing triggers, querying state, and retrieving state machine metadata.
/// </summary>
/// <typeparam name="TState">The type representing the states.</typeparam>
/// <typeparam name="TTrigger">The type representing the triggers.</typeparam>
public abstract class StateMachineGrain<TState, TTrigger> : Grain, IStateMachineGrain<TState, TTrigger>
{
    [NotNull]
    protected StateMachine<TState, TTrigger>? StateMachine { get; private set; }

    /// <summary>
    /// Activates the state machine.
    /// </summary>
    public Task ActivateAsync()
    {
        return StateMachine.ActivateAsync();
    }

    /// <summary>
    /// Deactivates the state machine.
    /// </summary>
    public Task DeactivateAsync()
    {
        return StateMachine.DeactivateAsync();
    }

    /// <summary>
    /// Fires the specified trigger asynchronously.
    /// </summary>
    /// <param name="trigger">The trigger to fire.</param>
    public Task FireAsync(TTrigger trigger)
    {
        return StateMachine.FireAsync(trigger);
    }

    /// <summary>
    /// Fires the specified trigger with one argument asynchronously.
    /// </summary>
    public async Task FireAsync<TArg0>(TTrigger trigger, TArg0 arg0)
    {
        var tp = StateMachine.SetTriggerParameters<TArg0>(trigger);
        await StateMachine.FireAsync(tp, arg0);
    }

    /// <summary>
    /// Fires the specified trigger with two arguments asynchronously.
    /// </summary>
    public async Task FireAsync<TArg0, TArg1>(TTrigger trigger, TArg0 arg0, TArg1 arg1)
    {
        var tp = StateMachine.SetTriggerParameters<TArg0, TArg1>(trigger);
        await StateMachine.FireAsync(tp, arg0, arg1);
    }

    /// <summary>
    /// Fires the specified trigger with three arguments asynchronously.
    /// </summary>
    public async Task FireAsync<TArg0, TArg1, TArg2>(TTrigger trigger, TArg0 arg0, TArg1 arg1, TArg2 arg2)
    {
        var tp = StateMachine.SetTriggerParameters<TArg0, TArg1, TArg2>(trigger);
        await StateMachine.FireAsync(tp, arg0, arg1, arg2);
    }

    /// <summary>
    /// Gets the current state asynchronously.
    /// </summary>
    public Task<TState> GetStateAsync()
    {
        return Task.FromResult(StateMachine.State);
    }

    /// <summary>
    /// Determines whether the state machine is in the specified state.
    /// </summary>
    public Task<bool> IsInStateAsync(TState state)
    {
        return Task.FromResult(StateMachine.IsInState(state));
    }

    /// <summary>
    /// Determines whether the specified trigger can be fired.
    /// </summary>
    public Task<bool> CanFireAsync(TTrigger trigger)
    {
        return Task.FromResult(StateMachine.CanFire(trigger));
    }

    /// <summary>
    /// Gets information about the state machine.
    /// </summary>
    public Task<OrleansStateMachineInfo> GetInfoAsync()
    {
        return Task.FromResult(new OrleansStateMachineInfo(StateMachine.GetInfo()));
    }

    /// <summary>
    /// Gets the permitted triggers for the current state.
    /// </summary>
    public Task<IEnumerable<TTrigger>> GetPermittedTriggersAsync(params object[] args)
    {
        return StateMachine.GetPermittedTriggersAsync(args);
    }

    /// <summary>
    /// Gets detailed information about permitted triggers for the current state.
    /// </summary>
    public async Task<IEnumerable<OrleansTriggerDetails<TTrigger>>> GetDetailedPermittedTriggersAsync(params object[] args)
    {
        var details = await StateMachine.GetDetailedPermittedTriggers(args);
        return details.Select(detail => new OrleansTriggerDetails<TTrigger>(
            detail.Trigger, detail.HasParameters, detail.Parameters?.ArgumentTypes.ToArray() ?? [])).ToArray();
    }

    /// <summary>
    /// Gets the permitted triggers property for the current state.
    /// </summary>
    public Task<IEnumerable<TTrigger>> GetPermittedTriggersPropertyAsync()
    {
        return StateMachine.PermittedTriggersAsync;
    }

    /// <summary>
    /// Determines whether the specified trigger can be fired and returns unmet guard conditions.
    /// </summary>
    public Task<(bool, ICollection<string>)> CanFireWithUnmetGuardsAsync(TTrigger trigger)
    {
        var result = StateMachine.CanFire(trigger, out var unmetGuards);
        return Task.FromResult((result, unmetGuards));
    }

    /// <summary>
    /// Determines whether the specified trigger with one argument can be fired.
    /// </summary>
    public Task<bool> CanFireAsync<TArg0>(TTrigger trigger, TArg0 arg0)
    {
        var tp = StateMachine.SetTriggerParameters<TArg0>(trigger);
        return Task.FromResult(StateMachine.CanFire(tp, arg0));
    }

    /// <summary>
    /// Determines whether the specified trigger with one argument can be fired and returns unmet guard conditions.
    /// </summary>
    public Task<(bool, ICollection<string>)> CanFireWithUnmetGuardsAsync<TArg0>(TTrigger trigger, TArg0 arg0)
    {
        var tp = StateMachine.SetTriggerParameters<TArg0>(trigger);
        var result = StateMachine.CanFire(tp, arg0, out var unmet);
        return Task.FromResult((result, unmet));
    }

    /// <summary>
    /// Determines whether the specified trigger with two arguments can be fired.
    /// </summary>
    public Task<bool> CanFireAsync<TArg0, TArg1>(TTrigger trigger, TArg0 arg0, TArg1 arg1)
    {
        var tp = StateMachine.SetTriggerParameters<TArg0, TArg1>(trigger);
        return Task.FromResult(StateMachine.CanFire(tp, arg0, arg1));
    }

    /// <summary>
    /// Determines whether the specified trigger with two arguments can be fired and returns unmet guard conditions.
    /// </summary>
    public Task<(bool, ICollection<string>)> CanFireWithUnmetGuardsAsync<TArg0, TArg1>(TTrigger trigger, TArg0 arg0, TArg1 arg1)
    {
        var tp = StateMachine.SetTriggerParameters<TArg0, TArg1>(trigger);
        var result = StateMachine.CanFire(tp, arg0, arg1, out var unmet);
        return Task.FromResult((result, unmet));
    }

    /// <summary>
    /// Determines whether the specified trigger with three arguments can be fired.
    /// </summary>
    public Task<bool> CanFireAsync<TArg0, TArg1, TArg2>(TTrigger trigger, TArg0 arg0, TArg1 arg1, TArg2 arg2)
    {
        var tp = StateMachine.SetTriggerParameters<TArg0, TArg1, TArg2>(trigger);
        return Task.FromResult(StateMachine.CanFire(tp, arg0, arg1, arg2));
    }

    /// <summary>
    /// Determines whether the specified trigger with three arguments can be fired and returns unmet guard conditions.
    /// </summary>
    public Task<(bool, ICollection<string>)> CanFireWithUnmetGuardsAsync<TArg0, TArg1, TArg2>(TTrigger trigger, TArg0 arg0, TArg1 arg1, TArg2 arg2)
    {
        var tp = StateMachine.SetTriggerParameters<TArg0, TArg1, TArg2>(trigger);
        var result = StateMachine.CanFire(tp, arg0, arg1, arg2, out var unmet);
        return Task.FromResult((result, unmet));
    }

    /// <summary>
    /// Returns a string representation of the state machine.
    /// </summary>
    public Task<string> ToStringAsync()
    {
        return Task.FromResult(StateMachine.ToString());
    }

    /// <summary>
    /// Builds the state machine instance.
    /// </summary>
    protected abstract StateMachine<TState, TTrigger> BuildStateMachine();

    /// <inheritdoc/>
    public override Task OnActivateAsync(CancellationToken cancellationToken)
    {
        StateMachine = BuildStateMachine();
        NotNull(StateMachine, nameof(StateMachine));
        return base.OnActivateAsync(cancellationToken);
    }

    /// <summary>
    /// Throws an exception if the provided object is null.
    /// </summary>
    /// <param name="obj">The object to check.</param>
    /// <param name="name">The name of the object.</param>
    private static void NotNull([NotNull] object? obj, string name)
    {
        if (obj == null)
            throw new InvalidOperationException($"{name} cannot be null");
    }
}
