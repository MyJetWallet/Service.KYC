using System;
using Grpc.Core;
using Grpc.Core.Interceptors;
using Grpc.Net.Client;
using JetBrains.Annotations;
using MyJetWallet.Sdk.GrpcMetrics;
using ProtoBuf.Grpc.Client;
using Service.Service.KYC.Grpc;

namespace Service.Service.KYC.Client
{
    [UsedImplicitly]
    public class KYCClientFactory
    {
        private readonly CallInvoker _channel;

        public KYCClientFactory(string assetsDictionaryGrpcServiceUrl)
        {
            AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);
            var channel = GrpcChannel.ForAddress(assetsDictionaryGrpcServiceUrl);
            _channel = channel.Intercept(new PrometheusMetricsInterceptor());
        }

        public IKycStatusService GetKycStatusService() => _channel.CreateGrpcService<IKycStatusService>();
    }
}
