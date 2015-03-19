using System;
using Contract;

namespace Service
{
    public class MyQueuedService : IMyQueuedService, IDisposable
    {
        public void DoAction1()
        {
            Console.WriteLine("Action1 performed!");
        }

        public void DoAction2(string message)
        {
            Console.WriteLine("Action2 performed! with message={0}", message);
        }

        public void DoAction3(Message message)
        {
            Console.WriteLine("MessageId={0}, MessageDescription={1}", message.Id, message.Description);
        }

        public void Dispose()
        {
            Console.WriteLine("Finished processing Message.");
        }
    }
}