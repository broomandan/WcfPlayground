﻿using System;
using Service.Contract;

namespace Service.Host
{
    public class MyQueuedService : IMyQueuedService
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
    }
}