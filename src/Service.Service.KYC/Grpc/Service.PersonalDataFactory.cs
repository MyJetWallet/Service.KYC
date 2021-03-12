using System;
using Grpc.Core;
using Grpc.Core.Interceptors;
using Grpc.Net.Client;
using JetBrains.Annotations;
using MyJetWallet.Sdk.GrpcMetrics;
using ProtoBuf.Grpc.Client;
using SimpleTrading.PersonalData.Grpc;

namespace Service.Service.KYC.Grpc
{
    [UsedImplicitly]
    public class PersonalDataFactory
    {
        private readonly CallInvoker _channel;

        public PersonalDataFactory(string personalDataGrpcServiceUrl)
        {
            AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);
            var channel = GrpcChannel.ForAddress(personalDataGrpcServiceUrl);
            _channel = channel.Intercept(new PrometheusMetricsInterceptor());
        }

        public IPersonalDataServiceGrpc GetPersonalDataServiceGrpc() => _channel.CreateGrpcService<IPersonalDataServiceGrpc>();
    }
}
