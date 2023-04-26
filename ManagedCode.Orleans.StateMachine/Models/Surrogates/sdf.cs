using System;
using System.Collections.Generic;
using System.Linq;
using Orleans;
using Stateless.Reflection;

namespace ManagedCode.Orleans.StateMachine.Models.Surrogates;

[GenerateSerializer]
[Immutable]
public readonly struct StateMachineInfoSurrogate
{
    public StateMachineInfoSurrogate(
        IEnumerable<StateInfo> states,
        Type stateType,
        Type triggerType,
        StateInfo initialState)
    {
        this.InitialState = initialState;
        this.States = (IEnumerable<StateInfo>) ((states != null ? states.ToList<StateInfo>() : (List<StateInfo>) null) ?? throw new ArgumentNullException(nameof (states)));
        this.StateType = stateType;
        this.TriggerType = triggerType;
    }

    /// <summary>Exposes the initial state of this state machine.</summary>
    public StateInfo InitialState { get; }

    /// <summary>
    /// Exposes the states, transitions, and actions of this machine.
    /// </summary>
    public IEnumerable<StateInfo> States { get; }

    /// <summary>The type of the underlying state.</summary>
    /// <returns></returns>
    public Type StateType { get; private set; }

    /// <summary>The type of the underlying trigger.</summary>
    /// <returns></returns>
    public Type TriggerType { get; private set; }
}