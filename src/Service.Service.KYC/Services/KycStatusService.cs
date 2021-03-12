using System;
using System.Threading.Tasks;
using MyNoSqlServer.Abstractions;
using Service.Service.KYC.Domain.Models;
using Service.Service.KYC.Grpc;
using Service.Service.KYC.Grpc.Models;
using Service.Service.KYC.Mappers;
using Service.Service.KYC.MyNoSql;
using SimpleTrading.PersonalData.Grpc;

namespace Service.Service.KYC.Services
{
    public class KycStatusService : IKycStatusService
    {
        private readonly IPersonalDataServiceGrpc _personalDataServiceGrpc;
        private readonly IMyNoSqlServerDataWriter<KycStatusNoSqlEntity> _myNoSqlWriter;

        public KycStatusService(IPersonalDataServiceGrpc personalDataServiceGrpc, IMyNoSqlServerDataWriter<KycStatusNoSqlEntity> myNoSqlWriter)
        {
            _personalDataServiceGrpc = personalDataServiceGrpc;
            _myNoSqlWriter = myNoSqlWriter;
        }

        public async ValueTask<KycStatusResponse> GetKycStatusAsync(KycStatusRequest request)
        {
            var personalDataResponse = await _personalDataServiceGrpc.GetByIdAsync(request.UserId);

            if (personalDataResponse.PersonalData == null)
            {
                return new KycStatusResponse();
            }

            var kycModel = new KycModel()
            {
                ClientId = personalDataResponse.PersonalData.Id,
                Status = KycStatusMapper.MapPersonalDataStatusToKycStatus(personalDataResponse.PersonalData.KYC)
            };

            var entity = KycStatusNoSqlEntity.Create(kycModel);
            await _myNoSqlWriter.InsertOrReplaceAsync(entity);

            return new KycStatusResponse()
            {
                ClientId = kycModel.ClientId,
                Status = kycModel.Status
            };
        }
    }
}