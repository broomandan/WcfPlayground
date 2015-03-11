using System;
using System.ServiceModel;
using System.ServiceModel.Description;
using Service.Contract;

namespace Service.Host
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            const string netTcpLocalhostBaseAddress = "net.tcp://localhost:8100/";
            const string httpLocalhostBaseAddress = "http://localhost:8200/";

            string tcpEndpointAddress = string.Format("{0}myqueuedService", netTcpLocalhostBaseAddress);

            var host = new ServiceHost(typeof (MyQueuedService),
                new Uri(netTcpLocalhostBaseAddress),
                new Uri(httpLocalhostBaseAddress));

            EnableMex(host);

            host.AddServiceEndpoint(typeof (IMyQueuedService), new NetTcpBinding(), tcpEndpointAddress);

            Console.WriteLine("Opening the Service");
            host.Open();
            Console.WriteLine("Service opened");
            Console.ReadLine();
        }


        private static void EnableMex(ServiceHost host)
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