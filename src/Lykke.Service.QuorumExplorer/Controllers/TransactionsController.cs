using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Lykke.Service.QuorumExplorer.Client;
using Lykke.Service.QuorumExplorer.Client.Models;
using Lykke.Service.QuorumExplorer.Domain.DTOs;
using Lykke.Service.QuorumExplorer.Domain.Services.Explorer;
using Microsoft.AspNetCore.Mvc;
using TransactionDetailedInfo = Lykke.Service.QuorumExplorer.Client.Models.TransactionDetailedInfo;

namespace Lykke.Service.QuorumExplorer.Controllers
{
    [ApiController]
    [Route("api/transactions")]
    public class TransactionsController : ControllerBase, ITransactionsApi
    {
        private readonly ITransactionsReader _transactionsReader;
        private readonly IEventsReader _eventsReader;

        public TransactionsController(ITransactionsReader transactionsReader, IEventsReader eventsReader)
        {
            _transactionsReader = transactionsReader;
            _eventsReader = eventsReader;
        }

        /// <summary>
        /// Get transactions filtered by conditions provided, paginated 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(PaginatedTransactionsResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<PaginatedTransactionsResponse> GetFilteredAsync(
            [FromBody] FilteredTransactionsRequest request)
        {
            var result = await _transactionsReader.GetFilteredAsync(
                new TransactionsFilter
                {
                    From = request.From,
                    To = request.To,
                    AffectedAddresses = request.AffectedAddresses,
                    FunctionName = request.FunctionName,
                    FunctionSignature = request.FunctionSignature
                },
                request.PagingInfo.CurrentPage,
                request.PagingInfo.PageSize);

            return Mapper.Map<PaginatedTransactionsResponse>(result);
        }

        /// <summary>
        /// Get events related to transaction
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet("events")]
        [ProducesResponseType(typeof(PaginatedEventsResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<PaginatedEventsResponse> GetEventsByTransactionAsync([FromQuery] TransactionEventsRequest request)
        {
            var result = await _eventsReader.GetByTransactionAsync(
                request.TransactionHash,
                request.CurrentPage,
                request.PageSize);

            return Mapper.Map<PaginatedEventsResponse>(result);
        }

        /// <summary>
        /// Get transaction details by hash
        /// </summary>
        /// <param name="transactionHash">The transaction hash</param>
        /// <returns></returns>
        [HttpGet("{transactionHash}")]
        [ProducesResponseType(typeof(TransactionDetailsResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<TransactionDetailsResponse> GetDetailsAsync(string transactionHash)
        {
            var result = await _transactionsReader.GetDetailsAsync(transactionHash);

            if (result == null)
                return new TransactionDetailsResponse { Error = TransactionsErrorCodes.TransactionDoesNotExist };

            return new TransactionDetailsResponse
            {
                Transaction = Mapper.Map<TransactionDetailedInfo>(result),
                Error = TransactionsErrorCodes.None
            };
        }
    }
}
