using System;
using System.Diagnostics;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;

namespace Proxy.ServiceBus.Proxy
{
    public class CustomQueuedServiceBusClient<TService> : IServiceBusProperties where TService : class
    {
        private readonly ServiceBusConfiguration<TService> _configuration;
        private readonly ServiceEndpoint _endpoint;

        public CustomQueuedServiceBusClient()
        {
            _configuration = new ServiceBusConfiguration<TService>();
            _endpoint = GetDefaultEndPoint();
        }

        public string SessionId { get; protected set; }

        protected TransportClientEndpointBehavior Credential
        {
            get
            {
                IServiceBusProperties properties = this;
                return properties.Credential;
            }
            set
            {
                IServiceBusProperties properties = this;
                properties.Credential = value;
            }
        }

        TransportClientEndpointBehavior IServiceBusProperties.Credential
        {
            get
            {
                if (_endpoint.Behaviors.Contains(typeof (TransportClientEndpointBehavior)))
                {
                    return _endpoint.Behaviors.Find<TransportClientEndpointBehavior>();
                }
                var credential = new TransportClientEndpointBehavior();
                Credential = credential;
                return Credential;
            }
            set
            {
                Debug.Assert(_endpoint.Behaviors.Contains(typeof (TransportClientEndpointBehavior)) == false);
                _endpoint.Behaviors.Add(value);
            }
        }

        Uri[] IServiceBusProperties.Addresses
        {
            get { return new[] {_endpoint.Address.Uri}; }
        }

        protected TService CreateChannel()
        {
            Debug.Assert(_endpoint.Binding is NetMessagingBinding);

            var requiresSession = SessionId != null;
            var properties = this as IServiceBusProperties;
            var tuple = ServiceBusHelper.ParseUri(_endpoint.Address.Uri);
            //TODO is this parsing still needed?
            ServiceBusHelper.VerifyQueue(tuple.Item1, tuple.Item2, properties.Credential.TokenProvider, requiresSession);

            var factory = new ChannelFactory<TService>(_endpoint);
            return factory.CreateChannel();
        }

        protected BrokeredMessageProperty GetMessageProperty(ref Message request)
        {
            BrokeredMessageProperty property;

            if (request.Properties.ContainsKey(BrokeredMessageProperty.Name) == false)
            {
                property = new BrokeredMessageProperty();
                request.Properties.Add(BrokeredMessageProperty.Name, property);
            }
            else
            {
                property = request.Properties[BrokeredMessageProperty.Name] as BrokeredMessageProperty;
            }
            return property;
        }

        private ServiceEndpoint GetDefaultEndPoint()
        {
            var endpoindUri = _configuration.Endpoint;
            var binding = GetNetMessagingBinding();
            var endpoint = GetEndpoint(endpoindUri, binding, typeof (TService));
            var tokenProvider = TokenProvider.CreateSharedAccessSignatureTokenProvider(_configuration.Issuer,
                                                                                       _configuration.Secret);
            var behavior = new TransportClientEndpointBehavior(tokenProvider);

            ServiceBusHelper.SetBehavior(new[] {endpoint}, behavior);
            return endpoint;
        }

        private static NetMessagingBinding GetNetMessagingBinding()
        {
            return new NetMessagingBinding
            {
                SendTimeout = TimeSpan.FromMinutes(3),
                ReceiveTimeout = TimeSpan.FromMinutes(3),
                OpenTimeout = TimeSpan.FromMinutes(3),
                CloseTimeout = TimeSpan.FromMinutes(3),
                TransportSettings = new NetMessagingTransportSettings {BatchFlushInterval = TimeSpan.FromSeconds(2)}
            };
        }

        private static ServiceEndpoint GetEndpoint(Uri endpointUri, Binding binding, Type service)
        {
            var address = new EndpointAddress(endpointUri);
            var contract = ContractDescription.GetContract(service);

            return new ServiceEndpoint(contract, binding, address);
        }
    }
}