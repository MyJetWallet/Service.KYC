using System;
using Service.Service.KYC.Domain.Models;

namespace Service.Service.KYC.Client
{
    public interface IKycStatusClient
    {
        IKycStatusResponse GetClientKycStatus(IKycStatusRequest request);
        
        event Action OnChanged;
    }
}