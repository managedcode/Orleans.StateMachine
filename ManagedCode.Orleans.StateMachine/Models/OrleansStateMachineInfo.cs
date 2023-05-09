using System.Collections.Generic;
using System.Linq;
using Orleans;
using Stateless.Reflection;

namespace ManagedCode.Orleans.StateMachine.Models;

[GenerateSerializer]
public class OrleansStateMachineInfo
{
    public OrleansStateMachineInfo(StateMachineInfo info)
    {
        InitialState = new OrleansStateInfo(info.InitialState);
        States = info.States.Select(s => new OrleansStateInfo(s)).ToList();
        StateType = info.StateType.FullName;
        TriggerType = info.TriggerType.FullName;
    }

    /// <summary>Exposes the initial state of this state machine.</summary>
    [Id(0)]
    public OrleansStateInfo InitialState { get; set; }

    /// <summary>
    ///     Exposes the states, transitions, and actions of this machine.
    /// </summary>
    [Id(1)]
    public List<OrleansStateInfo> States { get; set; }

    /// <summary>The type of the underlying state.</summary>
    /// <returns></returns>
    [Id(2)]
    public string StateType { get; set; }

    /// <summary>The type of the underlying trigger.</summary>
    /// <returns></returns>
    [Id(3)]
    public string TriggerType { get; set; }
}