using InvestmentApi.DTOs;

namespace InvestmentApi.Services
{
    /// <summary>
    /// Interface do serviço de investimentos definindo as operações disponíveis.
    /// </summary>
    public interface IInvestmentService
    {
        // Retorna todos os investimentos, opcionalmente filtrando por tipo.
        IEnumerable<InvestmentDto> GetInvestments(string? type = null);

        // Retorna um investimento específico pelo ID (ou null se não encontrado).
        InvestmentDto? GetInvestmentById(int id);

        // Realiza a simulação de investimento com base nos parâmetros fornecidos.
        SimulationResultDto SimulateInvestment(InvestmentSimulationDto simulation);
    }
}
