using System;

namespace Service.Host
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var hostFactory = new CustomServiceHostFactory();
            var host = hostFactory.Create();
            
            Console.WriteLine("Opening the Service");
            host.Open();
            Console.WriteLine("Service opened");
            Console.ReadLine();
        }
    }
}