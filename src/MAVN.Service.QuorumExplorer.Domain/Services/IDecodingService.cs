using MAVN.Service.QuorumExplorer.Domain.DTOs;

namespace MAVN.Service.QuorumExplorer.Domain.Services
{
    public interface IDecodingService
    {
        Event DecodeTransactionLog(
            TransactionLog transactionLog);
        
        FunctionCall DecodeTransactionInput(
            TransactionInput transactionInput);
    }
}
