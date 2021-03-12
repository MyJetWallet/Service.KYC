using System.Reflection;
using Autofac;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MyJetWallet.Sdk.GrpcMetrics;
using MyJetWallet.Sdk.GrpcSchema;
using MyJetWallet.Sdk.Service;
using MyNoSqlServer.DataReader;
using MyServiceBus.TcpClient;
using Prometheus;
using ProtoBuf.Grpc.Server;
using Service.Service.KYC.Grpc;
using Service.Service.KYC.Modules;
using Service.Service.KYC.Services;
using Service.Service.KYC.Settings;
using SimpleTrading.BaseMetrics;
using SimpleTrading.ServiceStatusReporterConnector;
using SimpleTrading.SettingsReader;

namespace Service.Service.KYC
{
    public class Startup
    {
        private readonly MyNoSqlTcpClient _myNoSqlClient;
        private readonly MyServiceBusTcpClient _serviceBusClient;

        public Startup()
        {
            _myNoSqlClient = new MyNoSqlTcpClient(
                () => SettingsReader.ReadSettings<SettingsModel>(Program.SettingsFileName).MyNoSqlWriterUrl,
                ApplicationEnvironment.HostName ??
                $"{ApplicationEnvironment.AppName}:{ApplicationEnvironment.AppVersion}");

            _serviceBusClient = new MyServiceBusTcpClient(Program.ReloadedSettings(model => model.ServiceBusUrl),
                ApplicationEnvironment.HostName);
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCodeFirstGrpc(options =>
            {
                options.Interceptors.Add<PrometheusMetricsInterceptor>();
                options.BindMetricsInterceptors();
            });

            services.AddHostedService<ApplicationLifetimeManager>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseMetricServer();

            app.BindServicesTree(Assembly.GetExecutingAssembly());

            app.BindIsAlive();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGrpcSchema<KycStatusService, IKycStatusService>();

                endpoints.MapGrpcSchemaRegistry();

                endpoints.MapGet("/",
                    async context =>
                    {
                        await context.Response.WriteAsync(
                            "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");
                    });
            });

            _myNoSqlClient.Start();
            _serviceBusClient.Start();
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule<SettingsModule>();
            builder.RegisterModule<ServiceModule>();
            builder.RegisterModule<ClientsModule>();
            builder.RegisterModule(new MyNoSqlModule(() => GetSettings().MyNoSqlWriterUrl));
            builder.RegisterModule(new ServiceBusModule(_serviceBusClient));
        }

        private SettingsModel GetSettings()
        {
            return SettingsReader.ReadSettings<SettingsModel>(Program.SettingsFileName);
        }
    }
}