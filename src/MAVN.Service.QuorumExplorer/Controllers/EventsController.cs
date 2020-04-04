using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using MAVN.Service.QuorumExplorer.Client;
using MAVN.Service.QuorumExplorer.Client.Models;
using MAVN.Service.QuorumExplorer.Domain.DTOs;
using MAVN.Service.QuorumExplorer.Domain.Services.Explorer;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace MAVN.Service.QuorumExplorer.Controllers
{
    [ApiController]
    [Route("api/events")]
    public class EventsController : ControllerBase, IEventsApi
    {
        private readonly IEventsReader _eventsReader;

        public EventsController(IEventsReader eventsReader)
        {
            _eventsReader = eventsReader;
        }
        
        /// <summary>
        /// Get transactions filtered by conditions provided, paginated 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(PaginatedEventsResponse), (int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.BadRequest)]
        [ProducesResponseType((int) HttpStatusCode.NoContent)]
        public async Task<PaginatedEventsResponse> GetFilteredAsync([FromBody] FilteredEventsRequest request)
        {
            var result = await _eventsReader.GetFilteredAsync(
                new EventsFilter
                {
                    Address = request.Address,
                    EventName = request.EventName,
                    EventSignature = request.EventSignature,
                    AffectedAddresses = request.AffectedAddresses
                }, 
                request.PagingInfo.CurrentPage, 
                request.PagingInfo.PageSize);

            return Mapper.Map<PaginatedEventsResponse>(result);
        }
    }
}
