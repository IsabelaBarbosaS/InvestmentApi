using System.ComponentModel.DataAnnotations;

namespace InvestmentApi.DTOs
{
    public class InvestmentSimulationDto
    {
        [Required]
        public decimal ValorInicial { get; set; }

        [Required]
        public DateTime Vencimento { get; set; }
    }
}
