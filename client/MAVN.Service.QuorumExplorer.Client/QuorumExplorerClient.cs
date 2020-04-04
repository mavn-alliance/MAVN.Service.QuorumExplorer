using Lykke.HttpClientGenerator;

namespace MAVN.Service.QuorumExplorer.Client
{
    /// <summary>
    ///    QuorumExplorer API aggregating interface.
    /// </summary>
    public class QuorumExplorerClient : IQuorumExplorerClient
    {
        public QuorumExplorerClient(
            IHttpClientGenerator httpClientGenerator)
        {
            ContractsApi = httpClientGenerator.Generate<IContractsApi>();
            EventsApi = httpClientGenerator.Generate<IEventsApi>();
            TransactionsApi = httpClientGenerator.Generate<ITransactionsApi>();
        }

        public IContractsApi ContractsApi { get; }
        public IEventsApi EventsApi { get; }
        public ITransactionsApi TransactionsApi { get; }
    }
}
