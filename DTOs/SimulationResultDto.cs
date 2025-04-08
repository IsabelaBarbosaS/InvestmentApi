namespace InvestmentApi.DTOs
{
    /// <summary>
    /// DTO que representa o resultado da simulação de investimento.
    /// </summary>
    public class SimulationResultDto
    {
        // Montante final após a simulação (valor futuro do investimento).
        public decimal FinalAmount { get; set; }

        // Total investido (montante inicial + todas as contribuições feitas).
        public decimal TotalInvested { get; set; }

        // Total de juros ganhos (FinalAmount - TotalInvested).
        public decimal TotalInterestEarned { get; set; }
    }
}
