using Microsoft.AspNetCore.Mvc;
using InvestmentApi.Services;
using InvestmentApi.DTOs;

namespace InvestmentApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InvestmentController : ControllerBase
    {
        private readonly IInvestmentService _investmentService;

        public InvestmentController(IInvestmentService investmentService)
        {
            _investmentService = investmentService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<InvestmentDto>> GetInvestments()
        {
            var investimentos = _investmentService.GetAll();
            return Ok(investimentos);
        }

        [HttpGet("filter")]
        public ActionResult<IEnumerable<InvestmentDto>> FilterByType([FromQuery] string tipo)
        {
            var filtrados = _investmentService.FilterByType(tipo);
            return Ok(filtrados);
        }

        [HttpPost("simular")]
        public ActionResult<SimulationResultDto> Simulate([FromBody] InvestmentSimulationDto input)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var resultado = _investmentService.Simulate(input);
            return Ok(resultado);
        }
    }
}
