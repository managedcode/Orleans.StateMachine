using System.Collections.Generic;
using System.Linq;
using Orleans;
using Stateless.Reflection;

namespace ManagedCode.Orleans.StateMachine.Models;

[GenerateSerializer]
public class OrleansStateInfo
{
    public OrleansStateInfo(StateInfo stateInfo)
    {
        UnderlyingState = stateInfo.UnderlyingState;

        // if(stateInfo.IgnoredTriggers is not null)
        //     IgnoredTriggers = new (stateInfo.IgnoredTriggers);
        //
        //
        // if(stateInfo.EntryActions is not null)
        //     EntryActions = new (stateInfo.EntryActions);
        //
        // if(stateInfo.ActivateActions is not null)
        //     ActivateActions = new (stateInfo.ActivateActions);
        //
        // if(stateInfo.DeactivateActions is not null)
        //     DeactivateActions = new (stateInfo.DeactivateActions);
        //
        // if(stateInfo.ExitActions is not null)
        //     ExitActions = new (stateInfo.ExitActions);

        if (stateInfo.Substates is not null)
            Substates = new List<OrleansStateInfo>(stateInfo.Substates.Select(s => new OrleansStateInfo(s)));

        if (stateInfo.Superstate is not null)
            Superstate = new OrleansStateInfo(stateInfo.Superstate);

        // if(stateInfo.FixedTransitions is not null)
        //     FixedTransitions =new ( stateInfo.FixedTransitions);
        //
        // if(stateInfo.DynamicTransitions is not null)
        //     DynamicTransitions = new (stateInfo.DynamicTransitions);
        //
        // if(stateInfo.IgnoredTriggers is not null)
        //     IgnoredTriggers =new ( stateInfo.IgnoredTriggers);
    }

    /// <summary>The instance or value this state represents.</summary>
    [Id(0)]
    public object UnderlyingState { get; }

    /// <summary>Substates defined for this StateResource.</summary>
    [Id(1)]
    public List<OrleansStateInfo> Substates { get; private set; }

    /// <summary>Superstate defined, if any, for this StateResource.</summary>
    [Id(2)]
    public OrleansStateInfo Superstate { get; private set; }

    // /// <summary>
    // /// Actions that are defined to be executed on state-entry.
    // /// </summary>
    // [Id(3)]
    // private List<ActionInfo> EntryActions { get; private set; }
    //
    // /// <summary>
    // /// Actions that are defined to be executed on activation.
    // /// </summary>
    // [Id(4)]
    // public List<InvocationInfo> ActivateActions { get; private set; }
    //
    // /// <summary>
    // /// Actions that are defined to be executed on deactivation.
    // /// </summary>
    // [Id(5)]
    // public List<InvocationInfo> DeactivateActions { get; private set; }
    //
    // /// <summary>
    // /// Actions that are defined to be exectuted on state-exit.
    // /// </summary>
    // [Id(6)]
    // public List<InvocationInfo> ExitActions { get; private set; }

    // /// <summary>Transitions defined for this state.</summary>
    // [Id(7)]
    // public List<FixedTransitionInfo> FixedTransitions { get; private set; }
    //
    // /// <summary>
    // /// Dynamic Transitions defined for this state internally.
    // /// </summary>
    // [Id(8)]
    // public List<DynamicTransitionInfo> DynamicTransitions { get; private set; }
    //
    // /// <summary>Triggers ignored for this state.</summary>
    // [Id(9)]
    // public List<IgnoredTransitionInfo> IgnoredTriggers { get; private set; }
    //
    // /// <summary>Transitions defined for this state.</summary>
    // public List<TransitionInfo> Transitions => (List<TransitionInfo>)FixedTransitions.Concat<TransitionInfo>(DynamicTransitions);


    /// <summary>Passes through to the value's ToString.</summary>
    public override string ToString()
    {
        return UnderlyingState?.ToString() ?? "<null>";
    }
}