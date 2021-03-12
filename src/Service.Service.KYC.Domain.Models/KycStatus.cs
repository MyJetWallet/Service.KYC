using System.Runtime.Serialization;

namespace Service.Service.KYC.Domain.Models
{
    [DataContract]
    public enum KycStatus
    {
        NotVerified,
        OnVerification,
        Verified,
        Restricted,
    }
}