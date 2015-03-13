using System.ServiceModel;
using Service.Contract;

namespace Service.Proxy
{
    public class MyQueuedServiceProxy : ClientBase<IMyQueuedService>, IMyQueuedService
    {
        public MyQueuedServiceProxy()
        {
        }

        public MyQueuedServiceProxy(string endpointConfigurationName) : base(endpointConfigurationName)
        {
        }

        public void DoAction1()
        {
            Channel.DoAction1();
        }

        public void DoAction2(string message)
        {
            Channel.DoAction2(message);
        }

        public void DoAction3(Message message)
        {
            Channel.DoAction3(message);
        }
    }
}