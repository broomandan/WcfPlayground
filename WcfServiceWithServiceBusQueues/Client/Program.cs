using System;
using System.ServiceModel.Channels;
using Contract;
using Message = Contract.Message;

namespace Client
{
    internal class Program
    {
        private const string ServiceQueueName = "queuedService";

        private static void Main(string[] args)
        {
            try
            {
                Console.Title = "Client for WCF serrvice using Service Bus Queues";
                var serviceProxy = new MyQueuedServiceProxy(ServiceQueueName);

                // Send messages

                Console.WriteLine("Sending message to {0}...", ServiceQueueName);
                var message = new Message {Id = 65, Description = "First Message"};

                serviceProxy.DoAction3(message);
                
            }
            catch (Exception exception)
            {
                Console.WriteLine("Exception occurred: {0}", exception);
            }
            
            Console.WriteLine("\nClient complete.");
            Console.WriteLine("\nPress [Enter] to exit.");
            Console.ReadLine();
        }
    }
}