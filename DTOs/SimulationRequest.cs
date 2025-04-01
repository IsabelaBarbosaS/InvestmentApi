namespace InvestmentApi.DTOs
{
    public class SimulationRequest
    {
        public decimal InitialAmount { get; set; }
        public int Months { get; set; }
        public decimal AnnualInterestRate { get; set; }
    }
}
