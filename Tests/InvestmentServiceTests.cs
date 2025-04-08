using InvestmentApi.DTOs;
using InvestmentApi.Services;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace Tests
{
    public class InvestmentServiceTests
    {
        [Fact]
        public void Simulate_ValidInput_ReturnsPositiveRentabilidade()
        {
            var logger = new Mock<ILogger<InvestmentService>>();
            var service = new InvestmentService(logger.Object);

            var input = new InvestmentSimulationDto
            {
                ValorInicial = 1000,
                Vencimento = DateTime.Today.AddDays(30)
            };

            var result = service.Simulate(input);

            Assert.True(result.ValorFinal > input.ValorInicial);
        }
    }
}
