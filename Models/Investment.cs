using System;

namespace InvestmentApi.Models
{
    public class Investment
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public decimal InterestRate { get; set; } // Anual
    }
}
