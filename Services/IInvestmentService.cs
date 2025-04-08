using InvestmentApi.DTOs;
using System.Collections.Generic;

namespace InvestmentApi.Services
{
    public interface IInvestmentService
    {
        /// <summary>
        /// Retorna todos os investimentos disponíveis.
        /// </summary>
        /// <returns>Lista de investimentos disponíveis.</returns>
        IEnumerable<InvestmentDto> GetAll();

        /// <summary>
        /// Simula o retorno de um investimento com base no valor inicial e na data de vencimento.
        /// </summary>
        /// <param name="input">Dados de entrada da simulação.</param>
        /// <returns>Resultado da simulação contendo valor final, rentabilidade e detalhes.</returns>
        SimulationResultDto Simulate(InvestmentSimulationDto input);
    }
}
