using System;
using System.Collections.Generic;

namespace Proxy.ServiceBus.Proxy
{
    internal class ServiceBusConfigurationItems
    {
        private static readonly IDictionary<string, ServiceBusConfigurationItem> Items =
            new Dictionary<string, ServiceBusConfigurationItem>
            {
                {
                    EnvironmentName.Local, new ServiceBusConfigurationItem
                    {
                        Namespace = "ci-platform-messaging",
                        Secret = "PEhn+9fXAs3tcY+pky6yLSg4qgSjhIk85FL/oQzzeI4="
                    }
                },
                {
                    EnvironmentName.Ci, new ServiceBusConfigurationItem
                    {
                        Namespace = "ci-platform-messaging",
                        Secret = "PEhn+9fXAs3tcY+pky6yLSg4qgSjhIk85FL/oQzzeI4="
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