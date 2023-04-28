using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ManagedCode.Orleans.StateMachine.Extensions;
using ManagedCode.Orleans.StateMachine.Interfaces;
using ManagedCode.Orleans.StateMachine.Models.Surrogates;
using Orleans;
using Stateless;
using Stateless.Reflection;

namespace ManagedCode.Orleans.StateMachine;

public abstract class StateMachineGrain<TState, TTrigger> : Grain, IStateMachineGrain<TState, TTrigger>
{
    protected readonly StateMachine<TState, TTrigger>.StateConfiguration _stateConfiguration;
    protected StateMachine<TState, TTrigger> StateMachine { get; private set; }

    protected abstract StateMachine<TState, TTrigger> BuildStateMachine();

    public override Task OnActivateAsync(CancellationToken cancellationToken)
    {
        StateMachine = BuildStateMachine();
        
        return base.OnActivateAsync(cancellationToken);
    }

    public Task ActivateAsync()
    {
        return StateMachine.ActivateAsync();
    }

    public Task DeactivateAsync()
    {
        return StateMachine.DeactivateAsync();
    }

    public Task FireAsync(TTrigger trigger)
    {
        return StateMachine.FireAsync(trigger);
    }
    
    public Task FireAsync<TArg0>(StateMachine<TState, TTrigger>.TriggerWithParameters<TArg0> trigger, TArg0 arg0)
    {
        return StateMachine.FireAsync(trigger, arg0);
    }

    public Task FireAsync<TArg0, TArg1>(StateMachine<TState, TTrigger>.TriggerWithParameters<TArg0, TArg1> trigger, TArg0 arg0, TArg1 arg1)
    {
        return StateMachine.FireAsync(trigger, arg0, arg1);
    }

    public Task FireAsync<TArg0, TArg1, TArg2>(StateMachine<TState, TTrigger>.TriggerWithParameters<TArg0, TArg1, TArg2> trigger, TArg0 arg0, TArg1 arg1, TArg2 arg2)
    {
        return StateMachine.FireAsync(trigger, arg0, arg1,arg2);
    }

    public Task<TState> GetStateAsync()
    {
        return Task.FromResult(StateMachine.State);
    }

    public Task<bool> IsInStateAsync(TState state)
    {
        return Task.FromResult(StateMachine.IsInState(state));
    }

    public Task<bool> CanFireAsync(TTrigger trigger)
    {
        return Task.FromResult(StateMachine.CanFire(trigger));
    }
    
    public Task<OrleansStateMachineInfo> GetInfoAsync()
    {
        return Task.FromResult(new OrleansStateMachineInfo(StateMachine.GetInfo()));
    }
}