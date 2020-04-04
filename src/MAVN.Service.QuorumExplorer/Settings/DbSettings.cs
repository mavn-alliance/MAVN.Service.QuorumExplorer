using Lykke.SettingsReader.Attributes;

namespace MAVN.Service.QuorumExplorer.Settings
{
    public class DbSettings
    {
        public string DataConnString { get; set; }
        
        [AzureTableCheck]
        public string LogsConnString { get; set; }
        
        public int CommandTimeoutSeconds { get; set; }
    }
}
