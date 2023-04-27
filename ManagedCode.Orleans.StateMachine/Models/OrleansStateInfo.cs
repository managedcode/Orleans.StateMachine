using System.Collections.Generic;
using Orleans;
using Stateless.Reflection;

namespace ManagedCode.Orleans.StateMachine.Models.Surrogates;

[GenerateSerializer]
public class OrleansStateInfo
{
    public OrleansStateInfo(StateInfo stateInfo)
    {
        UnderlyingState = stateInfo.UnderlyingState;
        IgnoredTriggers = stateInfo.IgnoredTriggers;
        EntryActions = stateInfo.EntryActions;
        ActivateActions = stateInfo.ActivateActions;
        DeactivateActions = stateInfo.DeactivateActions;
        ExitActions = stateInfo.ExitActions;
        Substates = stateInfo.Substates;
        Superstate = stateInfo.Superstate;
        
        Transitions = stateInfo.Transitions;
        FixedTransitions = stateInfo.FixedTransitions;
        DynamicTransitions = stateInfo.DynamicTransitions;
        IgnoredTriggers = stateInfo.IgnoredTriggers;
    }

    /// <summary>The instance or value this state represents.</summary>
    [Id(0)]
    public object UnderlyingState { get; }

    /// <summary>Substates defined for this StateResource.</summary>
    [Id(1)]
    public IEnumerable<StateInfo> Substates { get; private set; }

    /// <summary>Superstate defined, if any, for this StateResource.</summary>
    [Id(2)]
    public StateInfo Superstate { get; private set; }

    /// <summary>
    /// Actions that are defined to be executed on state-entry.
    /// </summary>
    [Id(3)]
    public IEnumerable<ActionInfo> EntryActions { get; private set; }

    /// <summary>
    /// Actions that are defined to be executed on activation.
    /// </summary>
    [Id(4)]
    public IEnumerable<InvocationInfo> ActivateActions { get; private set; }

    /// <summary>
    /// Actions that are defined to be executed on deactivation.
    /// </summary>
    [Id(5)]
    public IEnumerable<InvocationInfo> DeactivateActions { get; private set; }

    /// <summary>
    /// Actions that are defined to be exectuted on state-exit.
    /// </summary>
    [Id(6)]
    public IEnumerable<InvocationInfo> ExitActions { get; private set; }

    /// <summary>Transitions defined for this state.</summary>
    [Id(7)]
    public IEnumerable<TransitionInfo> Transitions { get;  private set; }

    /// <summary>Transitions defined for this state.</summary>
    [Id(8)]
    public IEnumerable<FixedTransitionInfo> FixedTransitions { get; private set; }

    /// <summary>
    /// Dynamic Transitions defined for this state internally.
    /// </summary>
    [Id(9)]
    public IEnumerable<DynamicTransitionInfo> DynamicTransitions { get; private set; }

    /// <summary>Triggers ignored for this state.</summary>
    [Id(10)]
    public IEnumerable<IgnoredTransitionInfo> IgnoredTriggers { get; private set; }

    /// <summary>Passes through to the value's ToString.</summary>
    public override string ToString() => this.UnderlyingState?.ToString() ?? "<null>";
}