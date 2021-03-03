using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Service.Service.KYC.Grpc;
using Service.Service.KYC.Grpc.Models;
using Service.Service.KYC.Settings;

namespace Service.Service.KYC.Services
{
    public class HelloService: IHelloService
    {
        private readonly ILogger<HelloService> _logger;

        public HelloService(ILogger<HelloService> logger)
        {
            _logger = logger;
        }

        public Task<HelloMessage> SayHelloAsync(HelloRequest request)
        {
            _logger.LogInformation("Hello from {name}", request.Name);

            return Task.FromResult(new HelloMessage
            {
                Message = "Hello " + request.Name
            });
        }
    }
}
