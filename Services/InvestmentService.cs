using InvestmentApi.DTOs;
using InvestmentApi.Models;

namespace InvestmentApi.Services
{
    public class InvestmentService : IInvestmentService
    {
        private readonly List<Investment> _investments;

        public InvestmentService()
        {
            // Lista simulada de investimentos (mock)
            _investments = new List<Investment>
            {
                new Investment
                {
                    Nome = "CDB Banco XP",
                    Tipo = "Renda Fixa",
                    ValorMinimo = 1000,
                    RentabilidadeAnual = 13.75, // % ao ano
                    Vencimento = DateTime.Today.AddMonths(12)
                },
                new Investment
                {
                    Nome = "Tesouro Direto",
                    Tipo = "Tesouro Prefixado",
                    ValorMinimo = 30,
                    RentabilidadeAnual = 10.5,
                    Vencimento = DateTime.Today.AddYears(2)
                }
            };
        }

        public IEnumerable<InvestmentDto> GetAll()
        {
            return _investments.Select(inv => new InvestmentDto
            {
                Nome = inv.Nome,
                Tipo = inv.Tipo,
                ValorMinimo = inv.ValorMinimo,
                RentabilidadeAnual = inv.RentabilidadeAnual,
                Vencimento = inv.Vencimento
            });
        }

        public SimulationResultDto Simulate(InvestmentSimulationDto input)
        {
            var dias = (input.Vencimento - DateTime.Today).Days;

            if (dias <= 0)
            {
                throw new ArgumentException("A data de vencimento deve ser futura.");
            }

            // Taxa fixa para simulação — na prática, poderia vir de um investimento escolhido
            var taxaAnual = 13.75;
            var taxaDiaria = taxaAnual / 100 / 365;

            var valorFinal = input.ValorInicial * (decimal)Math.Pow((1 + taxaDiaria), dias);
            var rentabilidade = valorFinal - input.ValorInicial;

            return new SimulationResultDto
            {
                ValorFinal = Math.Round(valorFinal, 2),
                Rentabilidade = Math.Round(rentabilidade, 2),
                TaxaUsada = taxaAnual,
                DiasInvestidos = dias
            };
        }
    }
}
