using Application.Features.MilitaryJets.Queries;
using Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<IActionResult> GetJets()
        {   
            var result = await _mediator.Send(new GetMilitaryJetsQuery());
            return HandleResult(result);
        }
    }
}
