using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;

namespace Service
{
    public class ErrorHandler : IErrorHandler
    {
        public bool HandleError(Exception error)
        {
            if (!error.GetType().Equals(typeof (CommunicationException)))
            {
                // Handle the exception as required by the application
                Console.WriteLine("Service encountered an exception.");
                Console.WriteLine(error.ToString());
            }

            return true;
        }

        public void ProvideFault(Exception error, MessageVersion version, ref Message fault)
        {
        }
    }
}