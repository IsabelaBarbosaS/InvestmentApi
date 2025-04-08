namespace InvestmentApi.DTOs
{
    /// <summary>
    /// DTO que representa os dados de um investimento para transferência (retorno via API).
    /// </summary>
    public class InvestmentDto
    {
        // Identificador do investimento.
        public int Id { get; set; }

        // Nome ou descrição do investimento.
        public string Name { get; set; }

        // Tipo ou categoria do investimento (ex: Ações, Títulos, Poupança).
        public string Type { get; set; }

        // Taxa de juros anual associada ao investimento (em percentual).
        public decimal AnnualInterestRate { get; set; }
    }
}
