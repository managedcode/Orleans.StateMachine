using Orleans.TestingHost;
using Xunit;

namespace ManagedCode.Orleans.StateMachine.Tests.Cluster;

[CollectionDefinition(nameof(TestClusterApplication))]
public class TestClusterApplication : ICollectionFixture<TestClusterApplication>
{
    public TestClusterApplication()
    {
        var builder = new TestClusterBuilder();
        builder.AddSiloBuilderConfigurator<TestSiloConfigurations>();
        builder.AddClientBuilderConfigurator<TestClientConfigurations>();
        Cluster = builder.Build();
        Cluster.Deploy();
    }

    public TestCluster Cluster { get; }
}