using Lykke.Service.QuorumExplorer.Domain.DTOs;

namespace Lykke.Service.QuorumExplorer.Domain.Services
{
    public interface IDecodingService
    {
        Event DecodeTransactionLog(
            TransactionLog transactionLog);
        
        FunctionCall DecodeTransactionInput(
            TransactionInput transactionInput);
    }
}
