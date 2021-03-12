using Service.Service.KYC.Domain.Models;
using SimpleTrading.Abstraction.PersonalData;

namespace Service.Service.KYC.Mappers
{
    public class KycStatusMapper
    {
        public static KycStatus MapPersonalDataStatusToKycStatus(PersonalDataKYCEnum? personalDataKycStatus)
        {
            if (personalDataKycStatus != null)
                return personalDataKycStatus switch
                {
                    PersonalDataKYCEnum.NotVerified => KycStatus.NotVerified,
                    PersonalDataKYCEnum.OnVerification => KycStatus.OnVerification,
                    PersonalDataKYCEnum.Verified => KycStatus.Verified,
                    PersonalDataKYCEnum.Restricted => KycStatus.Restricted,
                    _ => KycStatus.NotVerified
                };
            return KycStatus.NotVerified;
        }
    }
}