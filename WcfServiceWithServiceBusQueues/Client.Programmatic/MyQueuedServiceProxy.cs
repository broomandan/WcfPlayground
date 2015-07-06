using System.ServiceModel.Channels;
using Client.Programmatic.IFX.ServiceBus.Proxy;
using Contract;
using Message = Contract.Message;

namespace Client.Programmatic
{
    public class MyQueuedServiceProxy : ProgrammaticQueuedServiceBusClient<IMyQueuedService>, IMyQueuedService
    {

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

        public void DoAction3(Message message)
        {
            var channel = CreateChannel();
            (channel as IChannel).Open();

            channel.DoAction3(message);

            (channel as IChannel).Close();
        }
    }
}