using Autofac;
using Service.Service.KYC.Grpc;
using Service.Service.KYC.Jobs;
using Service.Service.KYC.Services;

namespace Service.Service.KYC.Modules
{
    public class ServiceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<KycStatusService>()
                .As<IKycStatusService>()
                .SingleInstance();

            builder
                .RegisterType<KycStatusChangeJob>()
                .AutoActivate()
                .SingleInstance();
        }
    }
}