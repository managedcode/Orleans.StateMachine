using System.Collections.Generic;
using System.Linq;
using Orleans;
using Stateless.Reflection;

namespace ManagedCode.Orleans.StateMachine.Models;

/// <summary>
/// Represents serializable state information for a state in a Stateless state machine,
/// including its substates and superstate, for use with Orleans.
/// </summary>
[GenerateSerializer]
public class OrleansStateInfo
{
    /// <summary>
    /// Initializes a new instance of the <see cref="OrleansStateInfo"/> class from a Stateless <see cref="StateInfo"/>.
    /// </summary>
    /// <param name="stateInfo">The state information to wrap.</param>
    public OrleansStateInfo(StateInfo stateInfo)
    {
        UnderlyingState = stateInfo.UnderlyingState;

        if (stateInfo.Substates is not null)
            Substates = new List<OrleansStateInfo>(stateInfo.Substates.Select(s => new OrleansStateInfo(s)));

        if (stateInfo.Superstate is not null)
            Superstate = new OrleansStateInfo(stateInfo.Superstate);
    }

    /// <summary>The instance or value this state represents.</summary>
    [Id(0)]
    public object UnderlyingState { get; }

    /// <summary>Substates defined for this StateResource.</summary>
    [Id(1)]
    public List<OrleansStateInfo> Substates { get; private set; } = [];

    /// <summary>Superstate defined, if any, for this StateResource.</summary>
    [Id(2)]
    public OrleansStateInfo? Superstate { get; private set; }


    /// <summary>
    /// Returns a string representation of the underlying state.
    /// </summary>
    /// <returns>The string representation of the underlying state or "&lt;null&gt;" if null.</returns>
    public override string ToString()
    {
        return UnderlyingState?.ToString() ?? "<null>";
    }
}
