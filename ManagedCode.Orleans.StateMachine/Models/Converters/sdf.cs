using ManagedCode.Orleans.StateMachine.Models.Surrogates;
using Orleans;
using Stateless.Reflection;

namespace ManagedCode.Orleans.StateMachine.Models.Converters;

[RegisterConverter]
public sealed class CompletionMessageConverter : IConverter<StateMachineInfo, StateMachineInfoSurrogate>
{
    public StateMachineInfo ConvertFromSurrogate(in StateMachineInfoSurrogate surrogate)
    {
        return new StateMachineInfo(
            surrogate.InvocationId,
            surrogate.Error,
            surrogate.Result,
            surrogate.HasResult);
    }

    public StateMachineInfoSurrogate ConvertToSurrogate(in StateMachineInfo value)
    {
        return new StateMachineInfoSurrogate(
            value.InvocationId,
            value.Error,
            value.Result,
            value.HasResult);
    }
}