using System;
using Contract;

namespace Client.Programmatic
{
    class Program
    {
        private static void Main(string[] args)
        {
            try
            {
                Console.Title = "Client for WCF serrvice using Service Bus Queues";
                var serviceProxy = new MyQueuedServiceProxy();

                Console.WriteLine("Sending message to Azure Service Bus Queue");
                var message = new Message { Id = 6888, Description = "Howdy" };

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
