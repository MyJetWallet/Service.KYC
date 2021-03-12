using MyNoSqlServer.Abstractions;
using Service.Service.KYC.Domain.Models;

namespace Service.Service.KYC.MyNoSql
{
    public class KycStatusNoSqlEntity : MyNoSqlDbEntity, IKycModel
    {
        public const string TableName = "myjetwallet-kyc-statuses";

        public static string GeneratePartitionKey(string brokerId) => $"broker:{brokerId}";
        public static string GenerateRowKey(string clientId) => clientId;

        public static KycStatusNoSqlEntity Create(IKycModel kycModel)
        {
            return new KycStatusNoSqlEntity()
            {
                PartitionKey = GeneratePartitionKey(kycModel.ClientId),
                RowKey = GenerateRowKey(kycModel.ClientId),
                ClientId = kycModel.ClientId,
                Status = kycModel.Status,
                BrokerId = kycModel.BrokerId
            };
        }

        public string BrokerId { get; set; }
        public string ClientId { get; set; }
        public KycStatus Status { get; set; }
    }
}