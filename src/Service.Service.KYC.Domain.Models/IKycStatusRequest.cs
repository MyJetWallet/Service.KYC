using System;

namespace Service.Service.KYC.Domain.Models
{
    public interface IKycStatusRequest
    {
        string UserId { get; set; }
    }
}
