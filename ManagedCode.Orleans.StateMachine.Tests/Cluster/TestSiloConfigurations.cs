using Orleans.TestingHost;

namespace ManagedCode.Orleans.StateMachine.Tests.Cluster;

public class TestSiloConfigurations : ISiloConfigurator
{
    public void Configure(ISiloBuilder siloBuilder)
    {
        //siloBuilder.AddOrleansRateLimiting();
    }
}