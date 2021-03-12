using System.Runtime.Serialization;
using Service.Service.KYC.Domain.Models;

namespace Service.Service.KYC.Grpc.Models
{
    [DataContract]
    public class KycStatusRequest: IKycStatusRequest
    {
        [DataMember(Order = 1)] public string BrokerId { get; set; }
        [DataMember(Order = 2)] public string ClientId { get; set; }
    }
}