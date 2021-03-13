using SimpleTrading.SettingsReader;

namespace Service.Service.KYC.Settings
{
    [YamlAttributesOnly]
    public class SettingsModel
    {
        [YamlProperty("KYC.SeqServiceUrl")]
        public string SeqServiceUrl { get; set; }
        
        [YamlProperty("KYC.KycServiceUrl")]
        public string KycServiceUrl { get; set; }
        
        [YamlProperty("KYC.MyNoSqlWriterUrl")]
        public string MyNoSqlWriterUrl { get; set; }
        
        [YamlProperty("KYC.ServiceBusUrl")]
        public string ServiceBusUrl { get; set; }

        [YamlProperty("KYC.DefaultBrokerId")]
        public string DefaultBrokerId { get; set; }
    }
}