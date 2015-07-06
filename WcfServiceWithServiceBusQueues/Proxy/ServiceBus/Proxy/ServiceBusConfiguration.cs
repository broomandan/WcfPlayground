using System;

namespace Proxy.ServiceBus.Proxy
{
    internal class ServiceBusConfiguration<T>
    {
        private static readonly ServiceBusConfigurationItems ConfigurationItems = new ServiceBusConfigurationItems();

        public ServiceBusConfiguration(string environment)
        {
            var configurationItem = ConfigurationItems[environment];

            Issuer = configurationItem.Issuer;
            Secret = configurationItem.Secret;
            Endpoint = GetEndpoint(environment, configurationItem.Namespace);
        }

        public ServiceBusConfiguration() : this(EnvironmentName.Current)
        {
        }

        public string Issuer { get; private set; }
        public string Secret { get; private set; }
        public Uri Endpoint { get; private set; }

        private static Uri GetEndpoint(string environment, string @namespace)
        {
            var typeName = typeof (T).Name.ToLowerInvariant();
            var isLocalEnvironment = environment == EnvironmentName.Local;
            var suffix = isLocalEnvironment
                             ? string.Format("-{0}", Environment.MachineName.ToLowerInvariant())
                             : string.Empty;

            var path = string.Format("{0}{1}", typeName, suffix);

            return new Uri(string.Format("sb://{0}.servicebus.windows.net/{1}",
                                         @namespace,
                                         path));
        }
    }
}