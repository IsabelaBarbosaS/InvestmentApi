using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using InvestmentApi.Services;
using InvestmentApi.DTOs;

namespace InvestmentApi.Controllers
{
    /// <summary>
    /// Controller responsável por gerenciar as requisições de investimento.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class InvestmentController : ControllerBase
    {
        private readonly IInvestmentService _investmentService;
        private readonly ILogger<InvestmentController> _logger;

        public InvestmentController(IInvestmentService investmentService, ILogger<InvestmentController> logger)
        {
            _investmentService = investmentService;
            _logger = logger;
        }

        /// <summary>
        /// Obtém todos os investimentos disponíveis, com opção de filtrar por tipo via querystring.
        /// </summary>
        [HttpGet]
        public ActionResult<IEnumerable<InvestmentDto>> GetInvestments([FromQuery] string? type = null)
        {
            // Chama o serviço para obter os investimentos (filtrados por tipo se fornecido).
            var investments = _investmentService.GetInvestments(type);
            return Ok(investments);
        }

        /// <summary>
        /// Obtém um investimento específico pelo ID.
        /// </summary>
        [HttpGet("{id}")]
        public ActionResult<InvestmentDto> GetInvestmentById(int id)
        {
            // Chama o serviço para obter um investimento pelo ID.
            var investmentDto = _investmentService.GetInvestmentById(id);
            if (investmentDto == null)
            {
                // Loga um aviso caso o investimento não seja encontrado e retorna 404.
                _logger.LogWarning($"Investment with id {id} not found.");
                return NotFound();
            }
            return Ok(investmentDto);
        }

        /// <summary>
        /// Realiza a simulação de um investimento com base nos parâmetros fornecidos.
        /// </summary>
        [HttpPost("simulate")]
        public ActionResult<SimulationResultDto> SimulateInvestment([FromBody] InvestmentSimulationDto simulation)
        {
            if (simulation == null)
            {
                // Loga um aviso e retorna erro 400 se os dados de simulação não forem fornecidos.
                _logger.LogWarning("Simulation request body is null.");
                return BadRequest("Simulation data is required.");
            }

            // Chama o serviço para calcular a simulação de investimento.
            var result = _investmentService.SimulateInvestment(simulation);
            return Ok(result);
        }
    }
}
