using System.ServiceModel;

namespace Service.Contract
{
    [ServiceContract]
    public interface IMyQueuedService
    {
        [OperationContract(IsOneWay = true)]
        void DoAction1();

        [OperationContract(IsOneWay = true)]
        void DoAction2(string message);

        [OperationContract(IsOneWay = true)]
        void DoAction3(Message message);
    }
}