using System;
using System.Messaging;
using System.ServiceModel;
using System.ServiceModel.Description;
using Service.Contract;

namespace Service.Host
{
    internal class CustomServiceHostFactory
    {
        public ServiceHost Create()
        {
            var netTcpLocalhostBaseAddress = new Uri("net.tcp://localhost:8100/");
            var httpLocalhostBaseAddress = new Uri("http://localhost:8200/");
            var msmqAddress = new Uri("net.msmq://localhost/private/MyQueuedServiceQueue");

            var host = new ServiceHost(typeof (MyQueuedService), netTcpLocalhostBaseAddress, httpLocalhostBaseAddress);

            AddMexEndpoints(host);

            AddTcpEndpoint(host, netTcpLocalhostBaseAddress);

            AddMsmqEndpoint(host, msmqAddress);
            return host;
        }

        private static void AddMsmqEndpoint(ServiceHost host, Uri serviceUri)
        {
            var queueName = QueuedServiceHelper.GetQueueFromUri(serviceUri);
            if (!MessageQueue.Exists(queueName))
            {
                MessageQueue.Create(queueName, true);
            }
            var netMsmqBinding = new NetMsmqBinding {Security = {Mode = NetMsmqSecurityMode.None}};
            host.AddServiceEndpoint(typeof (IMyQueuedService), netMsmqBinding, serviceUri);
        }

        private static void AddTcpEndpoint(ServiceHost host, Uri serviceUri)
        {
            var tcpEndpointAddress = string.Format("{0}myqueuedService", serviceUri);
            var netTcpBinding = new NetTcpBinding {Security = {Mode = SecurityMode.None}};
            host.AddServiceEndpoint(typeof (IMyQueuedService), netTcpBinding, tcpEndpointAddress);
        }


        private static void AddMexEndpoints(ServiceHost host)
        {
            var serviceMetadataBehavior = host.Description.Behaviors.Find<ServiceMetadataBehavior>();
            if (serviceMetadataBehavior == null)
            {
                var metadataBehavior = new ServiceMetadataBehavior {HttpGetEnabled = true};
                host.Description.Behaviors.Add(metadataBehavior);
            }

            host.AddServiceEndpoint(typeof (IMetadataExchange), MetadataExchangeBindings.CreateMexTcpBinding(), "MEX");
        }
    }
}