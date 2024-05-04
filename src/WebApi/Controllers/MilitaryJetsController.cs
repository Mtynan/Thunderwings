using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    public class MilitaryJetsController : BaseApiController
    {
        private readonly ILocalMemoryRepository _localMemoryRepository;
        public MilitaryJetsController(ILocalMemoryRepository localMemoryRepository)
        {
            _localMemoryRepository = localMemoryRepository;
        }

        [HttpGet]
        public IActionResult GetJets()
        {   
            var jets = _localMemoryRepository.GetJets();
            return Ok(jets);
        }
    }
}
