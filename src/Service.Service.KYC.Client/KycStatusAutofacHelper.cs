using Autofac;
using MyNoSqlServer.DataReader;
using Service.Service.KYC.Grpc;
using Service.Service.KYC.MyNoSql;

namespace Service.Service.KYC.Client
{
    public static class KycStatusAutofacHelper
    {
        /// <summary>
        /// Register interfaces:
        ///   * IKycStatusClient
        /// </summary>
        public static void RegisterKycStatusClients(this ContainerBuilder builder, IMyNoSqlSubscriber myNoSqlSubscriber, IKycStatusService kycStatusService)
        {
            var kycStatusMyNoSqlReadRepository = new MyNoSqlReadRepository<KycStatusNoSqlEntity>(myNoSqlSubscriber, KycStatusNoSqlEntity.TableName);
            
            builder
                .RegisterInstance(new KycStatusClient(kycStatusMyNoSqlReadRepository, kycStatusService))
                .As<IKycStatusClient>()
                .AutoActivate()
                .SingleInstance();
        }
    }
}