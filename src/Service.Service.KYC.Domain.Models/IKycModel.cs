namespace Service.Service.KYC.Domain.Models
{
    public interface IKycModel
    {
        string ClientId { get; set; }
        KycStatus Status { get; set; }
    }

    public class KycModel : IKycModel
    {
        public string ClientId { get; set; }
        public KycStatus Status { get; set; }
    }
}