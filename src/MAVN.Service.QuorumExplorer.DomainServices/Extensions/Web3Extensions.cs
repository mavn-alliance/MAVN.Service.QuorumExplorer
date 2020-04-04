using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;
using MoreLinq;
using Nethereum.Hex.HexTypes;
using Nethereum.RPC.Eth.Blocks;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.RPC.Eth.Services;

namespace MAVN.Service.QuorumExplorer.DomainServices.Extensions
{
    internal static class Web3Extensions
    {
        public static async Task<IReadOnlyCollection<TransactionReceipt>> GetReceiptsAsync(
            this IEthApiTransactionsService transactions,
            IEnumerable<string> hashes,
            int batchSize = 8)
        {
            var result = new List<TransactionReceipt>();
            
            foreach (var batchOfHashes in hashes.Batch(batchSize))
            {
                var batchOfTransactions = await Task.WhenAll
                (
                    batchOfHashes.Select(x => transactions.GetTransactionReceipt.SendRequestAsync(x))
                );

                result.AddRange(batchOfTransactions);
            }

            return result;
        }
        
        public static async Task<IReadOnlyCollection<Transaction>> GetTransactionsAsync(
            this IEthApiTransactionsService transactions,
            IEnumerable<string> hashes,
            int batchSize = 8)
        {
            var result = new List<Transaction>();
            
            foreach (var batchOfHashes in hashes.Batch(batchSize))
            {
                var batchOfTransactions = await Task.WhenAll
                (
                    batchOfHashes.Select(x => transactions.GetTransactionByHash.SendRequestAsync(x))
                );

                result.AddRange(batchOfTransactions);
            }

            return result;
        }
        
        public static Task<BlockWithTransactionHashes> SendRequestAsync(
            this IEthGetBlockWithTransactionsHashesByNumber ethGetBlockWithTransactionsHashesByNumber,
            long blockNumber,
            object id = null)
        {
            var bockNumberHex = new HexBigInteger(new BigInteger(blockNumber));

            return ethGetBlockWithTransactionsHashesByNumber.SendRequestAsync(bockNumberHex, id);
        }
    }
}
