using ManagedCode.Orleans.StateMachine.Extensions;
using ManagedCode.Orleans.StateMachine.Interfaces;
using Orleans;
using Stateless;
using Stateless.Reflection;
using System;
using System.Threading.Tasks;

namespace ManagedCode.Orleans.StateMachine
{
    public class StateGrain<TState, TEvent> : Grain, IGrain, IStateGrain<TState, TEvent>
    {
        private StateMachine<TState, TEvent> _stateMachine;
        private StateMachine<TState, TEvent>.StateConfiguration _stateConfiguration;

        public StateGrain(StateMachine<TState, TEvent>.StateConfiguration stateMachine, StateMachine<TState, TEvent> stateConfig)
        {
            _stateConfiguration = stateMachine;
            _stateMachine = stateConfig;
        }

        public Task<StateGrain<TState, TEvent>> OnEntryOrleansAsync(Func<Task> OnEntryOrleans)
        {
            _stateConfiguration.OnEntryOrleansAsync(OnEntryOrleans);

            return Task.FromResult(this);
        }

        public Task<StateGrain<TState, TEvent>> ConfigureAsync(TState state)
        {
            _stateMachine.Configure(state);

            return Task.FromResult(this);
        }

        public Task<StateGrain<TState, TEvent>> FireAsync(TEvent eventTrigger)
        {
            _stateMachine.Fire(eventTrigger);

            return Task.FromResult(this);
        }

        public Task<StateGrain<TState, TEvent>> PermitAsync(TEvent trigger, TState destinationState)
        {
            _stateConfiguration.Permit(trigger, destinationState);

            return Task.FromResult(this);
        }

        public Task<StateGrain<TState, TEvent>> PermitIfAsync(TEvent trigger, TState destinationState, Func<bool> guard, string guardDescription = null)
        {
            _stateConfiguration.PermitIf(trigger, destinationState, guard, guardDescription);

            return Task.FromResult(this);
        }

        public Task<StateGrain<TState, TEvent>> InternalTransitionIf(TEvent trigger, Func<object[], bool> guard, Action<StateMachine<TState, TEvent>.Transition> entryAction, string guardDescription = null)
        {
            _stateConfiguration.InternalTransitionIf(trigger, guard, entryAction, guardDescription);

            return Task.FromResult(this);
        }

        public Task<StateGrain<TState, TEvent>> InternalTransitionAsync(TEvent trigger, Action<StateMachine<TState, TEvent>.Transition> entryAction)
        {
            _stateConfiguration.InternalTransition(trigger, entryAction);

            return Task.FromResult(this);
        }


        public Task<StateGrain<TState, TEvent>> OnEntryAsync(Func<Task> entryAction)
        {
            _stateConfiguration.OnEntryAsync(entryAction);

            return Task.FromResult(this);
        }

        public Task<StateGrain<TState, TEvent>> OnEntryAsync(Action entryAction, string entryActionDescription = null)
        {
            _stateConfiguration.OnEntry(entryAction, entryActionDescription);

            return Task.FromResult(this);
        }

        public Task<StateGrain<TState, TEvent>> OnEntryAsync(Action<StateMachine<TState, TEvent>.Transition> entryAction, string entryActionDescription = null)
        {
            _stateConfiguration.OnEntry(entryAction, entryActionDescription);

            return Task.FromResult(this);
        }

        public Task<StateGrain<TState, TEvent>> OnEntryFromAsync(TEvent trigger, Func<Task> entryAction)
        {
            _stateConfiguration.OnEntryFromAsync(trigger, entryAction);

            return Task.FromResult(this);
        }

        public Task<StateGrain<TState, TEvent>> OnEntryFromAsync(TEvent trigger, Action entryAction, string entryActionDescription = null)
        {
            _stateConfiguration.OnEntryFrom(trigger, entryAction, entryActionDescription);

            return Task.FromResult(this);
        }        

        public Task<StateGrain<TState, TEvent>> OnEntryFromAsync(TEvent trigger, Action<StateMachine<TState, TEvent>.Transition> entryAction, string entryActionDescription = null)
        {
            _stateConfiguration.OnEntryFrom(trigger, entryAction, entryActionDescription);

            return Task.FromResult(this);
        }

        public Task<StateGrain<TState, TEvent>> OnExitAsync(Func<Task> exitAction)
        {
            _stateConfiguration.OnExitAsync(exitAction);

            return Task.FromResult(this);
        }

        public Task<StateGrain<TState, TEvent>> OnExitAsync(Action exitAction, string exitActionDescription = null)
        {
            _stateConfiguration.OnExit(exitAction, exitActionDescription);

            return Task.FromResult(this);
        }

        public Task<StateGrain<TState, TEvent>> OnExitAsync(Action<StateMachine<TState, TEvent>.Transition> exitAction, string exitActionDescription = null)
        {
            _stateConfiguration.OnExit(exitAction, exitActionDescription);

            return Task.FromResult(this);
        }

        public Task<StateGrain<TState, TEvent>> SubstateOfAsync(TState superstate)
        {
            _stateConfiguration.SubstateOf(superstate);

            return Task.FromResult(this);
        }

        public Task<StateGrain<TState, TEvent>> PermitReentryAsync(TEvent trigger)
        {
            _stateConfiguration.PermitReentry(trigger);

            return Task.FromResult(this);
        }

        public Task<StateGrain<TState, TEvent>> PermitReentryIfAsync(TEvent trigger, Func<bool> guard, string guardDescription = null)
        {
            _stateConfiguration.PermitReentryIf(trigger, guard, guardDescription);

            return Task.FromResult(this);
        }

        public Task<StateGrain<TState, TEvent>> PermitDynamicAsync(TEvent trigger, Func<TState> destinationStateSelector, string destinationStateSelectorDescription = null, DynamicStateInfos possibleDestinationStates = null)
        {
            _stateConfiguration.PermitDynamic(trigger, destinationStateSelector, destinationStateSelectorDescription, possibleDestinationStates);

            return Task.FromResult(this);
        }

        public Task<StateGrain<TState, TEvent>> PermitDynamicIfAsync(TEvent trigger, Func<TState> destinationStateSelector,
                Func<bool> guard, string guardDescription = null, Stateless.Reflection.DynamicStateInfos possibleDestinationStates = null)
        {
            _stateConfiguration.PermitDynamicIf(trigger, destinationStateSelector, guard, guardDescription, possibleDestinationStates);

            return Task.FromResult(this);
        }

        public Task<StateGrain<TState, TEvent>> PermitDynamicIfAsync(TEvent trigger, Func<TState> destinationStateSelector,
                string destinationStateSelectorDescription, Func<bool> guard, string guardDescription = null, Stateless.Reflection.DynamicStateInfos possibleDestinationStates = null)
        {
            _stateConfiguration.PermitDynamicIf(trigger, destinationStateSelector, destinationStateSelectorDescription, guard, guardDescription, possibleDestinationStates);

            return Task.FromResult(this);
        }



        public Task<StateGrain<TState, TEvent>> IgnoreAsync(TEvent trigger)
        {
            _stateConfiguration.Ignore(trigger);    

            return Task.FromResult(this);
        }

        public Task<StateGrain<TState, TEvent>> IgnoreIfAsync(TEvent trigger, Func<bool> guard, string guardDescription = null)
        {
            _stateConfiguration.IgnoreIf(trigger, guard, guardDescription);

            return Task.FromResult(this);
        }

        public Task<StateGrain<TState, TEvent>> OnActivateAsync(Action activateAction, string activateActionDescription = null)
        {
            _stateConfiguration.OnActivate(activateAction, activateActionDescription);

            return Task.FromResult(this);
        }

        public Task<StateGrain<TState, TEvent>> OnDeactivateAsync(Action deactivateAction, string deactivateActionDescription = null)
        {
            _stateConfiguration.OnDeactivate(deactivateAction, deactivateActionDescription);

            return Task.FromResult(this);
        }

        public Task<StateGrain<TState, TEvent>> SubstateOf(TState superstate)
        {
            _stateConfiguration.SubstateOf(superstate);

            return Task.FromResult(this);
        }        

        public Task<StateGrain<TState, TEvent>> InitialTransitionAsync(TState targetState)
        {
            _stateConfiguration.InitialTransition(targetState);

            return Task.FromResult(this);
        }


    }
}