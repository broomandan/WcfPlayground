using System;
using System.Collections.Generic;
using Microsoft.ServiceBus.Messaging;

namespace ServiceBusQueues.Sender
{
    internal class Program
    {
        private const string QueueName = "IssueTrackingQueue";
        private const string ServiceNamespace = "ur namespace";
        private const string SasKeyName = "RootManageSharedAccessKey";
        private const string SasKeyValue = "yoursecrete";

        private static void Main(string[] args)
        {
            var messageList = GenerateMessages();

            var sbHelper = new ServiceBusHelper(ServiceNamespace, SasKeyName, SasKeyValue, QueueName);

            var myQueueClient = sbHelper.CreateQueueClient(QueueName, ServiceNamespace);

            Console.WriteLine("Now sending messages to the SendToQueue.");
            SendToQueue(myQueueClient, messageList);
        }

        private static List<BrokeredMessage> GenerateMessages()
        {
            var issues = DataHelper.ParseCsvFile();
            return DataHelper.GenerateMessages(issues);
        }

        private static void SendToQueue(QueueClient queueClient, IReadOnlyList<BrokeredMessage> messageList)
        {
            if (queueClient == null) throw new ArgumentNullException("queueClient");

            for (var count = 0; count < 6; count++)
            {
                var issue = messageList[count];
                issue.Label = issue.Properties["IssueTitle"].ToString();
                queueClient.Send(issue);
                Console.WriteLine("Message sent: {0}, {1}", issue.Label, issue.MessageId);
            }
        }
    }
}