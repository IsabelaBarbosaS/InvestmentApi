namespace InvestmentApi.Models
{
    /// <summary>
    /// Modelo de domínio representando um investimento.
    /// </summary>
    public class Investment
    {
        // Identificador único do investimento.
        public int Id { get; set; }

        // Nome ou descrição do investimento.
        public string Name { get; set; }

        // Tipo ou categoria do investimento.
        public string Type { get; set; }

        // Taxa de juros anual (percentual) esperada para este investimento.
        public decimal AnnualInterestRate { get; set; }
    }
}
