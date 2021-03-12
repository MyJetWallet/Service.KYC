using SimpleTrading.SettingsReader;

namespace Service.Service.KYC.Settings
{
    [YamlAttributesOnly]
    public class SettingsModel
    {
        [YamlProperty("Service.KYC.SeqServiceUrl")]
        public string SeqServiceUrl { get; set; }
        
        [YamlProperty("Service.KYC.KycServiceUrl")]
        public string KycServiceUrl { get; set; }
        
        [YamlProperty("Service.KYC.MyNoSqlWriterUrl")]
        public string MyNoSqlWriterUrl { get; set; }
        
        [YamlProperty("Service.KYC.ServiceBusUrl")]
        public string ServiceBusUrl { get; set; }
    }
}