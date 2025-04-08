namespace InvestmentApi.DTOs
{
    public class SimulationResultDto
    {
        public decimal ValorFinal { get; set; }
        public decimal Rentabilidade { get; set; }
        public double TaxaUsada { get; set; } // Ex: CDI 13.75
        public int DiasInvestidos { get; set; }
    }
}
