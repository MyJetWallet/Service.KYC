using System.Runtime.Serialization;
using Service.Service.KYC.Domain.Models;

namespace Service.Service.KYC.Grpc.Models
{
    [DataContract]
    public class HelloMessage : IHelloMessage
    {
        [DataMember(Order = 1)]
        public string Message { get; set; }
    }
}