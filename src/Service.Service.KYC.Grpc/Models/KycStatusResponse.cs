using System.Runtime.Serialization;
using Service.Service.KYC.Domain.Models;

namespace Service.Service.KYC.Grpc.Models
{
    [DataContract]
    public class KycStatusResponse: IKycStatusResponse
    {
        [DataMember(Order = 1)] public string ClientId { get; set; }
        [DataMember(Order = 2)] public KycStatus Status { get; set; }
    }
}