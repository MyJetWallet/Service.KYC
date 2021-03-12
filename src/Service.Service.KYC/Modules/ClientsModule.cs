using Autofac;
using Service.Service.KYC.Grpc;
using SimpleTrading.PersonalData.Grpc;

namespace Service.Service.KYC.Modules
{
    public class ClientsModule: Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            PersonalDataClientFactory historyClientClientFactory = new PersonalDataClientFactory(Program.Settings.KycServiceUrl);
            
            builder
                .RegisterInstance(historyClientClientFactory.GetPersonalDataServiceGrpc())
                .As<IPersonalDataServiceGrpc>()
                .SingleInstance();
        }
    }
}