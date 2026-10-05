using System;
using Orleans;

namespace ManagedCode.Orleans.StateMachine.Models;

/// <summary>Serializable metadata for a permitted trigger and its expected arguments.</summary>
[GenerateSerializer]
public sealed record OrleansTriggerDetails<TTrigger>(
    [property: Id(0)] TTrigger Trigger,
    [property: Id(1)] bool HasParameters,
    [property: Id(2)] Type[] ArgumentTypes);
