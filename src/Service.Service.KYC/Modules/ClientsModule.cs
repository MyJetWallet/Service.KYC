using Autofac;
using Service.Service.KYC.Grpc;
using SimpleTrading.PersonalData.Grpc;

namespace Service.Service.KYC.Modules
{
    public class ClientsModule: Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            PersonalDataFactory historyClientFactory = new PersonalDataFactory(Program.Settings.KycServiceUrl);
            builder.RegisterInstance(historyClientFactory.GetPersonalDataServiceGrpc()).As<IPersonalDataServiceGrpc>().SingleInstance();
        }
    }
}