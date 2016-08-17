namespace NServiceBus.Core.Tests.Routing
{
    using NServiceBus.Routing;
    using NUnit.Framework;

    [TestFixture]
    public class DistributionPolicyTests
    {
        [Test]
        public void When_no_strategy_configured_for_endpoint_should_use_round_robbin_strategy()
        {
            IDistributionPolicy policy = new DistributionPolicy();

            var result = policy.GetDistributionStrategy("SomeEndpoint", DistributionStrategyScope.Sends);

            Assert.That(result, Is.TypeOf<SingleInstanceRoundRobinDistributionStrategy>());
        }

        [Test]
        public void When_strategy_configured_for_endpoint_should_use_configured_strategy()
        {
            var p = new DistributionPolicy();
            var configuredStrategy = new FakeDistributionStrategy("SomeEndpoint");
            p.SetDistributionStrategy(configuredStrategy, DistributionStrategyScope.Sends);

            IDistributionPolicy policy = p;
            var result = policy.GetDistributionStrategy("SomeEndpoint", DistributionStrategyScope.Sends);

            Assert.That(result, Is.EqualTo(configuredStrategy));
        }

        [Test]
        public void When_multiple_strategies_configured_endpoint_should_use_last_configured_strategy()
        {
            var p = new DistributionPolicy();
            var strategy = new FakeDistributionStrategy("SomeEndpoint");
            p.SetDistributionStrategy(new FakeDistributionStrategy("SomeEndpoint"), DistributionStrategyScope.Sends);
            p.SetDistributionStrategy(strategy, DistributionStrategyScope.Sends);
            p.SetDistributionStrategy(new FakeDistributionStrategy("SomeEndpoint"), DistributionStrategyScope.Publishes);

            IDistributionPolicy policy = p;
            var result = policy.GetDistributionStrategy("SomeEndpoint", DistributionStrategyScope.Sends);

            Assert.That(result, Is.EqualTo(strategy));
        }

        class FakeDistributionStrategy : DistributionStrategy
        {
            public FakeDistributionStrategy(string endpoint) : base(endpoint)
            {
            }

            public override string SelectReceiver(string[] receiverAddresses)
            {
                return null;
            }
        }
    }
}