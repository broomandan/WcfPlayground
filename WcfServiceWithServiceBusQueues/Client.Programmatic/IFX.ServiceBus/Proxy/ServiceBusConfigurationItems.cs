using System;
using System.Collections.Generic;

namespace Client.Programmatic.IFX.ServiceBus.Proxy
{
    internal class ServiceBusConfigurationItems
    {
        private static readonly IDictionary<string, ServiceBusConfigurationItem> Items =
            new Dictionary<string, ServiceBusConfigurationItem>
            {
                {
                    EnvironmentName.Local, new ServiceBusConfigurationItem
                    {
                        Namespace = "yourservicebusnamespace",
                        Secret = "yoursakeyForRootManageSharedAccessKey"
                    }
                },
                {
                    EnvironmentName.Ci, new ServiceBusConfigurationItem
                    {
                        Namespace = "yourservicebusnamespace",
                        Secret = "yoursakeyForRootManageSharedAccessKey"
                    }
                }
            };

        public ServiceBusConfigurationItem this[string environment]
        {
            get
            {
                ServiceBusConfigurationItem item;
                if (Items.TryGetValue(environment, out item))
                {
                    return item;
                }

                var message = string.Format("No ServiceBus configuration found. Environment={0}",
                                            environment);
                throw new InvalidOperationException(message);
            }
        }
    }
}