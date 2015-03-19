using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using Contract;

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


                var sendChannelFactory = new ChannelFactory<IMyQueuedService>(ServiceQueueName);
                var clientChannel = sendChannelFactory.CreateChannel();
                ((IChannel) clientChannel).Open();

                // Send messages

                Console.WriteLine("Sending message to {0}...", ServiceQueueName);
                var message = new Contract.Message() {Id = 65, Description = "First Message"};
                clientChannel.DoAction3(message);


                // Close sender
                ((IChannel) clientChannel).Close();
                sendChannelFactory.Close();
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