namespace InvestmentApi.DTOs
{
    public class InvestmentDto
    {
        public string Nome { get; set; }
        public string Tipo { get; set; }
        public decimal ValorMinimo { get; set; }
        public double RentabilidadeAnual { get; set; } // Ex: CDI, 110%
        public DateTime Vencimento { get; set; }
    }
}
