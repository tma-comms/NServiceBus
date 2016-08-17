namespace NServiceBus
{
    using System;
    using System.Linq;
    using Routing;

    class SpecificInstanceDistributionPolicy : IDistributionPolicy
    {
         string specificInstanceAddress;

        //TODO parameter!
        public SpecificInstanceDistributionPolicy(string specificInstanceAddress)
        {
            this.specificInstanceAddress = specificInstanceAddress;
        }

        public DistributionStrategy GetDistributionStrategy(string endpointName, DistributionStrategyScope scope)
        {
            return new SpecificInstanceDistributionStrategy(endpointName, specificInstanceAddress);
        }

        class SpecificInstanceDistributionStrategy : DistributionStrategy
        {
            public SpecificInstanceDistributionStrategy(string endpointName, string specificInstanceAddress) : base(endpointName)
            {
                this.specificInstanceAddress = specificInstanceAddress;
            }

            public override string SelectReceiver(string[] receiverAddresses)
            {
                var target = receiverAddresses.FirstOrDefault(t => t == specificInstanceAddress);
                if (target == null)
                {
                    throw new Exception($"Specified instance {specificInstanceAddress} has not been configured in the routing tables.");
                }
                return target;
            }

            string specificInstanceAddress;
        }
    }
}