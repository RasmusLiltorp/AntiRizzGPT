using Microsoft.AspNetCore.Mvc;
using AntiRizzGPT.Models;
using AntiRizzGPT.Services;

namespace AntiRizzGPT.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RizzController : ControllerBase
    {
        private readonly RizzService _rizzService;

        public RizzController(RizzService rizzService)
        {
            _rizzService = rizzService;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] RizzRequest request)
        {
            var result = await _rizzService.GenerateRizzAsync(request.Message);
            return Ok(new RizzResponse { Response = result });
        }
        
        [HttpGet("ping")]
        public IActionResult Ping()
        {
            return Ok("RizzController is online");
        }

    }
}