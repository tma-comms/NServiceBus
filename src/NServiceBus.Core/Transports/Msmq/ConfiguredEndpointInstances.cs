namespace NServiceBus
{
    using System.Collections.Generic;
    using Features;
    using Routing;

    class ConfiguredEndpointInstancesFeature : Feature
    {
        protected internal override void Setup(FeatureConfigurationContext context)
        {
            var collection = context.Settings.Get<ConfiguredEndpointInstances>();
            if (collection != null)
            {
                var instances = context.Settings.Get<EndpointInstances>();
                instances.AddOrReplaceInstances("EndpointConfiguration", collection);
            }
        }
    }

    class ConfiguredEndpointInstances : List<EndpointInstance>
    {
    }
}