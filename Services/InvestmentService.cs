using System;
using System.Collections.Generic;
using System.Linq;
using InvestmentApi.Models;
using InvestmentApi.DTOs;
using Microsoft.Extensions.Logging;

namespace InvestmentApi.Services
{
    /// <summary>
    /// Implementação do serviço de investimentos, contendo lógica de negócio como filtro e simulação.
    /// </summary>
    public class InvestmentService : IInvestmentService
    {
        private readonly ILogger<InvestmentService> _logger;
        private readonly List<Investment> _investments;  // Lista interna simulando um repositório de investimentos.

        public InvestmentService(ILogger<InvestmentService> logger)
        {
            _logger = logger;

            // Inicializa alguns dados de exemplo na lista de investimentos.
            _investments = new List<Investment>
            {
                new Investment { Id = 1, Name = "High Yield Savings", Type = "Savings", AnnualInterestRate = 4.5m },
                new Investment { Id = 2, Name = "Index Fund", Type = "Stock", AnnualInterestRate = 8m },
                new Investment { Id = 3, Name = "Corporate Bond", Type = "Bond", AnnualInterestRate = 5m }
            };

            _logger.LogInformation("InvestmentService initialized with sample data.");
        }

        public IEnumerable<InvestmentDto> GetInvestments(string? type = null)
        {
            // Loga se vai filtrar ou não.
            _logger.LogInformation(type == null 
                ? "Retrieving all investments." 
                : $"Filtering investments by type: {type}");

            IEnumerable<Investment> query = _investments;
            if (!string.IsNullOrEmpty(type))
            {
                // Aplica filtro por tipo (comparação case-insensitive).
                query = query.Where(inv => inv.Type.Equals(type, StringComparison.OrdinalIgnoreCase));
            }

            // Mapeia os Investments de domínio para InvestmentDto (dados de transferência).
            var resultList = query.Select(inv => new InvestmentDto 
            {
                Id = inv.Id,
                Name = inv.Name,
                Type = inv.Type,
                AnnualInterestRate = inv.AnnualInterestRate
            }).ToList();

            return resultList;
        }

        public InvestmentDto? GetInvestmentById(int id)
        {
            _logger.LogInformation($"Retrieving investment by ID: {id}");
            // Procura o investimento na lista pelo Id.
            var investment = _investments.FirstOrDefault(inv => inv.Id == id);
            if (investment == null)
            {
                _logger.LogWarning($"Investment not found: {id}");
                return null;
            }

            // Retorna o investimento encontrado mapeado para DTO.
            return new InvestmentDto
            {
                Id = investment.Id,
                Name = investment.Name,
                Type = investment.Type,
                AnnualInterestRate = investment.AnnualInterestRate
            };
        }

        public SimulationResultDto SimulateInvestment(InvestmentSimulationDto simulation)
        {
            _logger.LogInformation(
                $"Simulating investment: Initial={simulation.InitialAmount:C}, Monthly={simulation.MonthlyContribution:C}, " +
                $"Years={simulation.Years}, Rate={simulation.AnnualInterestRate}%"
            );

            // Converte a taxa anual em taxa mensal (dividindo por 100 para percentual e por 12 para meses).
            decimal monthlyRate = simulation.AnnualInterestRate / 100m / 12m;
            int totalMonths = simulation.Years * 12;
            decimal totalAmount = simulation.InitialAmount;

            // Itera mês a mês realizando contribuições e aplicando juros compostos mensalmente.
            for (int month = 1; month <= totalMonths; month++)
            {
                // Adiciona a contribuição do mês.
                totalAmount += simulation.MonthlyContribution;
                // Aplica o juros daquele mês sobre o total acumulado.
                totalAmount *= (1 + monthlyRate);
            }

            // Calcula o total investido (montante inicial + soma de todas contribuições mensais).
            decimal totalInvested = simulation.InitialAmount + simulation.MonthlyContribution * totalMonths;
            // Calcula o total de juros ganhos.
            decimal totalInterest = totalAmount - totalInvested;

            _logger.LogInformation(
                $"Simulation complete: Final Amount={totalAmount:C2}, Total Invested={totalInvested:C2}, Interest Earned={totalInterest:C2}"
            );

            // Retorna o resultado da simulação preenchendo o DTO de resultado.
            return new SimulationResultDto
            {
                FinalAmount = Math.Round(totalAmount, 2),      // Arredonda para 2 casas decimais (valor monetário final).
                TotalInvested = totalInvested,                 // Total investido durante o período.
                TotalInterestEarned = totalInterest            // Juros totais acumulados.
            };
        }
    }
}
