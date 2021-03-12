using Autofac;
using DotNetCoreDecorators;
using MyJetWallet.Sdk.Service;
using MyServiceBus.Abstractions;
using MyServiceBus.TcpClient;
using SimpleTrading.Abstraction.PersonalDataUpdate;
using SimpleTrading.ServiceBus.PublisherSubscriber.PersonalDataUpdate;

namespace Service.Service.KYC.Modules
{
    public class ServiceBusModule : Module
    {
        private readonly MyServiceBusTcpClient _serviceBusClient;

        public ServiceBusModule(MyServiceBusTcpClient serviceBusClient)
        {
            _serviceBusClient = serviceBusClient;
        }

        protected override void Load(ContainerBuilder builder)
        {
            var queue = $"KycStatusUpdates-{ApplicationEnvironment.HostName}";
            builder.RegisterInstance(new PersonalDataUpdateMyServiceBusSubscriber(_serviceBusClient, queue, TopicQueueType.Permanent, false))
                .As<ISubscriber<IPersonalDataUpdate>>()
                .SingleInstance();
        }
    }
}