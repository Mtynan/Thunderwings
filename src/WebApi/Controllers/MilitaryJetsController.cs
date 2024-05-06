using Application.Features.MilitaryJets.Queries;
using Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebApi.Requests.MilitaryJets;

namespace WebApi.Controllers
{
    /// <summary>
    /// Manages Military Jets.
    /// </summary>
    public class MilitaryJetsController : BaseApiController
    {
        private readonly IMediator _mediator;
        public MilitaryJetsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        /// <summary>
        /// Retrieves a list of military jets based on specified filtering criteria
        /// </summary>
        /// <param name="req">The filtering criteria for military jets.</param>
        /// <response code="200">Successful operation.</response>
        /// <response code="204">Successful operation with no results.</response>
        [HttpGet]
        public async Task<IActionResult> GetJets([FromQuery] GetMilitaryJetsRequest req)
        {   
            var result = await _mediator.Send(new GetMilitaryJetsQuery(
                req.Name, req.Manufacturer, req.Country, req.Role, req.TopSpeed, req.Price,
                req.PageNumber, req.PageSize));
            return HandleResult(result);
        }
    }
}
