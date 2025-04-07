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

        /// <summary>
        /// Retorna todos os investimentos disponíveis.
        /// </summary>
        [HttpGet]
        public ActionResult<IEnumerable<InvestmentDto>> GetInvestments()
        {
            var investimentos = _investmentService.GetAll();
            return Ok(investimentos);
        }

        /// <summary>
        /// Simula o retorno do investimento com base no valor e data de vencimento.
        /// </summary>
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
