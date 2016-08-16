namespace NServiceBus
{
    using Routing;

    /// <summary>
    /// Provides MSMQ-specific extensions to routing.
    /// </summary>
    public static class EndpointInstanceExtensions
    {
        /// <summary>
        /// Returns an endpoint instance bound to a given machine name.
        /// </summary>
        /// <param name="instance">A plain instance.</param>
        /// <param name="machineName">Machine name.</param>
        public static EndpointInstance AtMachine(this EndpointInstance instance, string machineName)
        {
            Guard.AgainstNull(nameof(instance), instance);
            Guard.AgainstNullAndEmpty(nameof(machineName), machineName);
            return instance.SetProperty("machine", machineName);
        }

        /// <summary>
        /// Returns an endpoint instance bound to a specific queue (instead of default to endpoint name as a queue name).
        /// </summary>
        /// <param name="instance">A plain instance.</param>
        /// <param name="queueName">Queue name.</param>
        public static EndpointInstance WithQueue(this EndpointInstance instance, string queueName)
        {
            Guard.AgainstNull(nameof(instance), instance);
            Guard.AgainstNullAndEmpty(nameof(queueName), queueName);
            return instance.SetProperty("queue", queueName);
        }
    }
}