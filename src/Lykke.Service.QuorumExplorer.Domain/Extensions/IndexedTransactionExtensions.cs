using Lykke.Service.QuorumExplorer.Domain.DTOs;

namespace Lykke.Service.QuorumExplorer.Domain.Extensions
{
    public static class IndexedTransactionExtensions
    {
        private const int SignatureLength = 10;

        public static string GetInputSignature(
            this IndexedTransaction indexedTransaction)
        {
            return indexedTransaction.Input.Length >= SignatureLength
                 ? indexedTransaction.Input.Substring(0, SignatureLength)
                 : null;
        }
    }
}
