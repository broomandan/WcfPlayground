using System;
using System.ServiceModel;

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

                
                var serviceHost = new ServiceHost(typeof (MyQueuedService));

                serviceHost.Description.Behaviors.Add(new ErrorServiceBehavior());

                serviceHost.Faulted += new EventHandler(serviceHost_Faulted);

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
        private static void serviceHost_Faulted(object sender, EventArgs e)
        {
            Console.WriteLine("Fault occurred. Aborting the service host object ...");
            ((ServiceHost) sender).Abort();
        }
    }
}