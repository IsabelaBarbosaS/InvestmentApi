using InvestmentApi.DTOs;

namespace InvestmentApi.Services
{
    public interface IInvestmentService
    {
        IEnumerable<InvestmentDto> GetAll();
        SimulationResultDto Simulate(InvestmentSimulationDto input);
    }
}
