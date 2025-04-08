using System;
using System.ComponentModel.DataAnnotations;

namespace InvestmentApi.DTOs
{
    public class InvestmentSimulationDto
    {
        [Required(ErrorMessage = "O valor inicial é obrigatório.")]
        [Range(1, double.MaxValue, ErrorMessage = "O valor inicial deve ser maior que zero.")]
        public decimal ValorInicial { get; set; }

        [Required(ErrorMessage = "A data de vencimento é obrigatória.")]
        public DateTime Vencimento { get; set; }
    }
}
