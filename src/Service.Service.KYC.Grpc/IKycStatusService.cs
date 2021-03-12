using System.ServiceModel;
using System.Threading.Tasks;
using Service.Service.KYC.Grpc.Models;

namespace Service.Service.KYC.Grpc
{
    [ServiceContract]
    public interface IKycStatusService
    {
        [OperationContract] ValueTask<KycStatusResponse> GetKycStatusAsync(KycStatusRequest request);
    }
}