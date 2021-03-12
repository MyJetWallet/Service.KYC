using System;
using System.Threading.Tasks;
using DotNetCoreDecorators;
using Microsoft.Extensions.Logging;
using MyNoSqlServer.Abstractions;
using Service.Service.KYC.Domain.Models;
using Service.Service.KYC.Mappers;
using Service.Service.KYC.MyNoSql;
using SimpleTrading.Abstraction.PersonalDataUpdate;
using SimpleTrading.PersonalData.Grpc;

namespace Service.Service.KYC.Jobs
{
    public class KycStatusChangeJob : IDisposable
    {
        private readonly IPersonalDataServiceGrpc _personalDataServiceGrpc;
        private readonly IMyNoSqlServerDataWriter<KycStatusNoSqlEntity> _myNoSqlWriter;
        private readonly ILogger<KycStatusChangeJob> _logger;

        public KycStatusChangeJob(ISubscriber<IPersonalDataUpdate> personalDataUpdateSubscriber,
            IPersonalDataServiceGrpc personalDataServiceGrpc,
            IMyNoSqlServerDataWriter<KycStatusNoSqlEntity> myNoSqlWriter,
            ILogger<KycStatusChangeJob> logger)
        {
            _myNoSqlWriter = myNoSqlWriter;
            _personalDataServiceGrpc = personalDataServiceGrpc;
            _logger = logger;
            personalDataUpdateSubscriber.Subscribe(HandleKycStatusUpdate);
        }

        private async ValueTask HandleKycStatusUpdate(IPersonalDataUpdate kycStatusUpdate)
        {
            var existingEntity = await _myNoSqlWriter.GetAsync(
                KycStatusNoSqlEntity.GeneratePartitionKey(kycStatusUpdate.TraderId),
                KycStatusNoSqlEntity.GenerateRowKey(kycStatusUpdate.TraderId));

            if (existingEntity == null)
            {
                //do not update kyc status if it is not in cache
                return;
            }

            var personalDataResponse = await _personalDataServiceGrpc.GetByIdAsync(kycStatusUpdate.TraderId);

            if (personalDataResponse.PersonalData == null)
            {
                return;
            }

            var kycModel = new KycModel()
            {
                ClientId = personalDataResponse.PersonalData.Id,
                Status = KycStatusMapper.MapPersonalDataStatusToKycStatus(personalDataResponse.PersonalData.KYC)
            };

            var entity = KycStatusNoSqlEntity.Create(kycModel);
            await _myNoSqlWriter.InsertOrReplaceAsync(entity);
            _logger.LogDebug(
                $"[ClientID:{entity.ClientId}] KycStatus updated from {existingEntity.Status} to {entity.Status}");
        }

        public void Dispose()
        {
        }
    }
}