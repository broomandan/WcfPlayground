using System.Runtime.Serialization;

namespace Service.Contract
{
    [DataContract]
    public class Message
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Description { get; set; }
    }
}