using System;
using System.Threading;
using Microsoft.ServiceBus.Messaging;
using ServiceBus.Management;

namespace ServiceBusQueues.Receiver
{
    internal class Program
    {
        private const string QueueName = "IssueTrackingQueue";
        private const string ServiceNamespace = "yournamespaceName";
        private const string SasKeyName = "RootManageSharedAccessKey";
        private const string SasKeyValue = "yoursaskeyvalue";

        private static void Main(string[] args)
        {
            var sbHelper = new ServiceBusHelper(ServiceNamespace, SasKeyName, SasKeyValue, QueueName);
            var queueClient = sbHelper.CreateQueueClient(QueueName);

            Console.WriteLine("Now receiving messages from Queue.");
            ReceiveFromQueue(queueClient);
            Console.ReadLine();
        }

        private static void ReceiveFromQueue(QueueClient queueClient)
        {
            BrokeredMessage message;
            while ((message = queueClient.Receive(new TimeSpan(hours: 0, minutes: 1, seconds: 5))) != null)
            {
                Console.WriteLine(string.Format("Message received: {0}, {1}, {2}", message.SequenceNumber, message.Label,
                    message.MessageId));
                message.Complete();

                Console.WriteLine("Processing message (sleeping...)");
                Thread.Sleep(1000);
            }
        }
    }
}