using System;
using ServiceModelEx.ServiceBus.Hosts;

namespace Service
{
    internal class Program
    {
        private const string QueueName = "Azure Service Bus queue";

        private static void Main(string[] args)
        {
            try
            {
                Console.Title = "MyQueuedService Service";
                Console.WriteLine("Ready to receive messages from {0}...", QueueName);


                var serviceHost = new QueuedServiceBusHost(typeof (MyQueuedService));

                serviceHost.Open();

                Console.WriteLine("\nPress [Enter] to close ServiceHost.");
                Console.ReadLine();

                serviceHost.Close();
            }
            catch (Exception exception)
            {
                Console.WriteLine("Exception occurred: {0}", exception);
                Console.WriteLine("\nPress [Enter] to exit.");
                Console.ReadLine();
            }
        }
    }
}