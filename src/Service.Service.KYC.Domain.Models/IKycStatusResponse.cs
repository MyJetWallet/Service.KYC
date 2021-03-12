using System;
using System.Runtime.Serialization;

namespace Service.Service.KYC.Domain.Models
{
    public interface IKycStatusResponse
    {
        string ClientId { get; set; }
        KycStatus Status { get; set; }
    }
}
