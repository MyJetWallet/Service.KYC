using System.Runtime.Serialization;
using Service.Service.KYC.Domain.Models;

namespace Service.Service.KYC.Grpc.Models
{
    [DataContract]
    public class KycStatusResponse: IKycStatusResponse
    {
        public KycStatusResponse()
        {
        }

        public KycStatusResponse(string brokerId, string clientId, KycStatus status)
        {
            BrokerId = brokerId;
            ClientId = clientId;
            Status = status;
        }

        [DataMember(Order = 1)] public string BrokerId { get; set; }
        [DataMember(Order = 2)] public string ClientId { get; set; }
        [DataMember(Order = 3)] public KycStatus Status { get; set; }
    }
}