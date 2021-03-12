using System;
using MyNoSqlServer.DataReader;
using Service.Service.KYC.Domain.Models;
using Service.Service.KYC.Grpc;
using Service.Service.KYC.Grpc.Models;
using Service.Service.KYC.MyNoSql;

namespace Service.Service.KYC.Client
{
    public class KycStatusClient : IKycStatusClient
    {
        private readonly MyNoSqlReadRepository<KycStatusNoSqlEntity> _readerKycStatuses;
        private readonly IKycStatusService _kycStatusService;

        public KycStatusClient(MyNoSqlReadRepository<KycStatusNoSqlEntity> readerKycStatuses, IKycStatusService kycStatusService)
        {
            _readerKycStatuses = readerKycStatuses;
            _kycStatusService = kycStatusService;

            _readerKycStatuses.SubscribeToUpdateEvents(list => Changed(), list => Changed());
        }

        public IKycStatusResponse GetClientKycStatus(IKycStatusRequest request)
        {
            var existingEntity = _readerKycStatuses.Get(
                KycStatusNoSqlEntity.GeneratePartitionKey(request.ClientId),
                KycStatusNoSqlEntity.GenerateRowKey(request.ClientId));
            if (existingEntity != null)
            {
                return new KycStatusResponse()
                {
                    ClientId = existingEntity.ClientId,
                    Status = existingEntity.Status
                };
            }

            var entity = _kycStatusService.GetKycStatusAsync(new KycStatusRequest() {ClientId = request.ClientId}).Result;
            return entity;
        }

        public event Action OnChanged;

        private void Changed()
        {
            OnChanged?.Invoke();
        }
    }
}