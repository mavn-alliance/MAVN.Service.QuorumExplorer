using JetBrains.Annotations;

namespace MAVN.Service.QuorumExplorer.Client
{
    /// <summary>
    ///    QuorumExplorer client interface.
    /// </summary>
    [PublicAPI]
    public interface IQuorumExplorerClient
    {
        /// <summary>
        ///    Contracts API interface.
        /// </summary>
        IContractsApi ContractsApi { get; }

        /// <summary>
        ///     Events API interface
        /// </summary>
        IEventsApi EventsApi { get; }
        
        /// <summary>
        ///     Transactions API interface
        /// </summary>
        ITransactionsApi TransactionsApi { get; }
    }
}
