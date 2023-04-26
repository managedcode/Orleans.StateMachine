using System;
using System.Threading.Tasks;
using Stateless;

namespace ManagedCode.Orleans.StateMachine.Extensions;

public static class StateMachineExtensions
{
    public static StateMachine<TState, TEvent>.StateConfiguration OnEntryOrleansAsync<TState, TEvent>(
        this StateMachine<TState, TEvent>.StateConfiguration machine,
        Func<Task> entryAction, string entryActionDescription = null)
    {
        return machine.OnEntryAsync(() => Task.Factory.StartNew(entryAction), entryActionDescription);
    }
}