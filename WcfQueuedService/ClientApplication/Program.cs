using System;
using Service.Proxy;

namespace ClientApplication
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var myQueuedServiceProxy = new MyQueuedServiceProxy("NetMsmqBinding_IMyQueuedService");

            Console.WriteLine("Making request of DoAction1 to Service");
            myQueuedServiceProxy.DoAction1();
            Console.WriteLine("Request of DoAction1 to Service completed");

            Console.WriteLine("Making request of DoAction2 to Service");
            myQueuedServiceProxy.DoAction2("Muhahahah");
            Console.WriteLine("Request of DoAction2 to Service completed");

            myQueuedServiceProxy.Close();

            Console.ReadLine();
        }
    }
}