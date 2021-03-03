using SimpleTrading.SettingsReader;

namespace Service.Service.KYC.Settings
{
    [YamlAttributesOnly]
    public class SettingsModel
    {
        [YamlProperty("Service.KYC.SeqServiceUrl")]
        public string SeqServiceUrl { get; set; }
    }
}