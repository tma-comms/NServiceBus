namespace NServiceBus
{
    using System;
    using System.Collections.Concurrent;
    using Routing;

    /// <summary>
    /// Allows to configure distribution strategies.
    /// </summary>
    public class DistributionPolicy : IDistributionPolicy
    {
        /// <summary>
        /// Sets the distribution strategy for a given endpoint.
        /// </summary>
        /// <param name="distributionStrategy">Distribution strategy to be used.</param>
        /// <param name="scope">Defines whether to use the specific strategy for distribute sends or publishes.</param>
        public void SetDistributionStrategy(DistributionStrategy distributionStrategy, DistributionStrategyScope scope)
        {
            Guard.AgainstNull(nameof(distributionStrategy), distributionStrategy);

            switch (scope)
            {
                case DistributionStrategyScope.Publishes:
                    configuredPublishStrategies[distributionStrategy.Endpoint] = distributionStrategy;
                    break;
                case DistributionStrategyScope.Sends:
                    configuredSendStrategies[distributionStrategy.Endpoint] = distributionStrategy;
                    break;
            }
        }

        DistributionStrategy IDistributionPolicy.GetDistributionStrategy(string endpointName, DistributionStrategyScope scope)
        {
            switch (scope)
            {
                case DistributionStrategyScope.Publishes:
                    return configuredPublishStrategies.GetOrAdd(endpointName, key => new SingleInstanceRoundRobinDistributionStrategy(key));
                case DistributionStrategyScope.Sends:
                    return configuredSendStrategies.GetOrAdd(endpointName, key => new SingleInstanceRoundRobinDistributionStrategy(key));
                default:
                    throw new ArgumentException($"{nameof(DistributionStrategyScope)} value {scope} is not handled by the policy.");
            }
        }

        ConcurrentDictionary<string, DistributionStrategy> configuredSendStrategies = new ConcurrentDictionary<string, DistributionStrategy>();
        ConcurrentDictionary<string, DistributionStrategy> configuredPublishStrategies = new ConcurrentDictionary<string, DistributionStrategy>();
    }
}