namespace Service.Service.KYC.Domain.Models
{
    public interface IKycModel
    {
        string BrokerId { get; }
        string ClientId { get; }
        KycStatus Status { get; }
    }

    public class KycModel : IKycModel
    {
        public KycModel()
        {
        }

        public KycModel(string brokerId, string clientId, KycStatus status)
        {
            BrokerId = brokerId;
            ClientId = clientId;
            Status = status;
        }

        public string BrokerId { get; set; }
        public string ClientId { get; set; }
        public KycStatus Status { get; set; }
    }
}