using JetBrains.Annotations;
using Lykke.SettingsReader.Attributes;

namespace MAVN.Service.QuorumExplorer.Client 
{
    /// <summary>
    ///    QuorumExplorer client settings.
    /// </summary>
    [PublicAPI]
    public class QuorumExplorerServiceClientSettings 
    {
        /// <summary>
        ///    Service url.
        /// </summary>
        [HttpCheck("api/isalive")]
        public string ServiceUrl {get; set;}
    }
}
