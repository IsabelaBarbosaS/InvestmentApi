namespace InvestmentApi.Models
{
    public class Investment
    {
        public string Nome { get; set; }
        public string Tipo { get; set; } // Ex: CDB, Tesouro Direto
        public decimal ValorMinimo { get; set; }
        public double RentabilidadeAnual { get; set; } // Percentual ao ano, ex: 13.75
        public DateTime Vencimento { get; set; }
    }
}
