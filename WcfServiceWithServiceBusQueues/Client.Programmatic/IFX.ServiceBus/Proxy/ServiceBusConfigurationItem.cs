namespace Client.Programmatic.IFX.ServiceBus.Proxy
{
    internal class ServiceBusConfigurationItem
    {
        public ServiceBusConfigurationItem()
        {
            Issuer = "RootManageSharedAccessKey";
        }

        public string Namespace { get; set; }
        public string Issuer { get; set; }
        public string Secret { get; set; }
    }
}