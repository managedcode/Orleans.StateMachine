using Microsoft.Extensions.DependencyInjection;

namespace ManagedCode.Orleans.StateMachine.Extensions;

public static class BuilderExtensions
{
    public static IServiceCollection AddOrleansStateMachine(this IServiceCollection clientBuilder)
    {
        return clientBuilder;
    }
    
}