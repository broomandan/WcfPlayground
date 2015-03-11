using System.ServiceModel;
using Service.Contract;

namespace Service.Proxy
{
    public class MyQueuedServiceProxy : ClientBase<IMyQueuedService>, IMyQueuedService
    {
        public void DoAction1()
        {
            Channel.DoAction1();
        }

        public void DoAction2(string message)
        {
            Channel.DoAction2(message);
        }
    }
}