using System;

namespace Service.Service.KYC.Domain.Models
{
    public interface IKycStatusRequest
    {
        string ClientId { get; set; }
    }
}
