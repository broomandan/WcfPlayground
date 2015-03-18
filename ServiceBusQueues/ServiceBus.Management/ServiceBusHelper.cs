using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;

namespace ServiceBus.Management
{
    public class ServiceBusHelper
    {
        private readonly TokenProvider _tokenProvider;
        private readonly string _serviceNamespace;

        public ServiceBusHelper(string serviceNamespace, string sasKeyName, string sasKeyValue, string queueName)
        {
            _serviceNamespace = serviceNamespace;
            _tokenProvider = TokenProvider.CreateSharedAccessSignatureTokenProvider(sasKeyName, sasKeyValue);
            CreateQueue(queueName);
        }


        public void CreateQueue(string queueName)
        {
            var namespaceClient =
                new NamespaceManager(ServiceBusEnvironment.CreateServiceUri("sb", _serviceNamespace, string.Empty),
                    _tokenProvider);

            if (!namespaceClient.QueueExists(queueName))
            {
                namespaceClient.CreateQueue(queueName);
            }
        }

        public QueueClient CreateQueueClient(string queueName)
        {
            var factory =
                MessagingFactory.Create(ServiceBusEnvironment.CreateServiceUri("sb", _serviceNamespace, string.Empty),
                    _tokenProvider);

            var myQueueClient = factory.CreateQueueClient(queueName);
            return myQueueClient;
        }
    }
}