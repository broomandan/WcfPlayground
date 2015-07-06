using System.ServiceModel.Channels;
using Contract;
using ServiceModelEx.ServiceBus.Proxies;

namespace Client
{
    public class MyQueuedServiceProxy : QueuedServiceBusClient<IMyQueuedService>, IMyQueuedService
    {
        public MyQueuedServiceProxy(string endpointName, string sessionId = null) : base(endpointName, sessionId)
        {
        }

        public void DoAction1()
        {
            var channel = CreateChannel();
            (channel as IChannel).Open();
            channel.DoAction1();
            (channel as IChannel).Close();
        }

        public void DoAction2(string message)
        {
            var channel = CreateChannel();
            (channel as IChannel).Open();

            channel.DoAction2(message);

            (channel as IChannel).Close();
        }

        public void DoAction3(Contract.Message message)
        {
            var channel = CreateChannel();
            (channel as IChannel).Open();

            channel.DoAction3(message);

            (channel as IChannel).Close();
        }
    }
}