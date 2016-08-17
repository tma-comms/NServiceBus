namespace NServiceBus.Routing
{
    /// <summary>
    /// Governs to which instances of a given endpoint a message is to be sent.
    /// </summary>
    public abstract class DistributionStrategy
    {
        /// <summary>
        /// Creates a new <see cref="DistributionStrategy"/>.
        /// </summary>
        /// <param name="endpoint">The endpoint name this distribution strategy was resolved for.</param>
        protected DistributionStrategy(string endpoint)
        {
            Guard.AgainstNullAndEmpty(nameof(endpoint), endpoint);

            Endpoint = endpoint;
        }

        /// <summary>
        /// Selects a destination instance for a message from all known addresses of a logical endpoint.
        /// </summary>
        public abstract string SelectReceiver(string[] receiverAddresses);

        /// <summary>
        /// The endpoint name this distribution strategy was resolved for.
        /// </summary>
        public string Endpoint { get; private set; }
    }
}