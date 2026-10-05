# Orleans.StateMachine

reference:
https://github.com/scottctr/NStateManager
https://github.com/dotnet-state-machine/stateless
## Version 10.4 migration

Version 10.4 targets .NET 10 and Orleans 10.4. Detailed permitted triggers now return
serializable `OrleansTriggerDetails<TTrigger>` values instead of Stateless's
`TriggerDetails<TState, TTrigger>`. Use `Trigger`, `HasParameters`, and
`ArgumentTypes` to inspect the trigger configuration across Orleans calls.
