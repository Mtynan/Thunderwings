using Application.Features.MilitaryJets.Queries;
using Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebApi.Requests.MilitaryJets;

namespace WebApi.Controllers
{
    public class MilitaryJetsController : BaseApiController
    {
        private readonly IMediator _mediator;
        public MilitaryJetsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetJets([FromQuery] GetMilitaryJetsRequest req)
        {   
            var result = await _mediator.Send(new GetMilitaryJetsQuery(req.Name, req.Manufacturer, req.Country, req.Role, req.TopSpeed, req.Price));
            return HandleResult(result);
        }
    }
}
