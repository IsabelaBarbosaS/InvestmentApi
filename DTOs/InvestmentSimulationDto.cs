namespace InvestmentApi.DTOs
{
    /// <summary>
    /// DTO com os dados necessários para simular um investimento ao longo do tempo.
    /// </summary>
    public class InvestmentSimulationDto
    {
        // Montante inicial investido.
        public decimal InitialAmount { get; set; }

        // Contribuição mensal adicional ao investimento.
        public decimal MonthlyContribution { get; set; }

        // Duração da simulação em anos.
        public int Years { get; set; }

        // Taxa de juros anual em percentual para simulação.
        public decimal AnnualInterestRate { get; set; }
    }
}
