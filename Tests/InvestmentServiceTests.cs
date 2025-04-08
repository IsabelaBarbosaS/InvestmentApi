using Xunit;
using Moq;
using InvestmentApi.Services;
using InvestmentApi.DTOs;
using Microsoft.Extensions.Logging;

namespace InvestmentApi.Tests
{
    /// <summary>
    /// Testes unitários para o InvestmentService usando xUnit e Moq.
    /// </summary>
    public class InvestmentServiceTests
    {
        [Fact]
        public void GetInvestments_WithType_FiltersByType()
        {
            // Arrange: cria o serviço com um Logger falso (mockado).
            var loggerMock = new Mock<ILogger<InvestmentService>>();
            var service = new InvestmentService(loggerMock.Object);

            // Act: obtém investimentos filtrando por tipo "Stock".
            var result = service.GetInvestments("Stock");

            // Assert: todos os investimentos retornados devem ser do tipo "Stock".
            Assert.NotEmpty(result);
            Assert.All(result, inv => Assert.Equal("Stock", inv.Type, ignoreCase: true));

            // Verifica se um log de informação sobre filtragem foi registrado.
            loggerMock.Verify(logger => logger.Log(
                    It.Is<LogLevel>(lvl => lvl == LogLevel.Information),
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((obj, t) => obj.ToString().Contains("Filtering investments by type")),
                    It.IsAny<Exception>(),
                    It.IsAny<Func<It.IsAnyType, Exception, string>>()),
                Times.Once,
                "Deveria ter logado informação de filtro por tipo uma vez.");
        }

        [Fact]
        public void GetInvestmentById_NotFound_ReturnsNull()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<InvestmentService>>();
            var service = new InvestmentService(loggerMock.Object);

            // Act: tenta obter um investimento com ID inexistente.
            var result = service.GetInvestmentById(999);

            // Assert: o resultado deve ser nulo, indicando não encontrado.
            Assert.Null(result);

            // Verifica se um log de aviso foi registrado para investimento não encontrado.
            loggerMock.Verify(logger => logger.Log(
                    It.Is<LogLevel>(lvl => lvl == LogLevel.Warning),
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((obj, t) => obj.ToString().Contains("Investment not found")),
                    It.IsAny<Exception>(),
                    It.IsAny<Func<It.IsAnyType, Exception, string>>()),
                Times.Once,
                "Deveria ter logado um aviso de investimento não encontrado.");
        }

        [Fact]
        public void GetInvestmentById_Existing_ReturnsInvestment()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<InvestmentService>>();
            var service = new InvestmentService(loggerMock.Object);

            // Act: obtém um investimento existente (ID 1, que existe nos dados de exemplo).
            var result = service.GetInvestmentById(1);

            // Assert: o resultado não deve ser nulo e os campos devem corresponder.
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
            Assert.Equal("High Yield Savings", result.Name);
            Assert.Equal("Savings", result.Type);
        }

        [Fact]
        public void SimulateInvestment_ZeroInterestRate_NoGrowth()
        {
            // Arrange: configura simulação com taxa de juros zero.
            var loggerMock = new Mock<ILogger<InvestmentService>>();
            var service = new InvestmentService(loggerMock.Object);
            var simulation = new InvestmentSimulationDto
            {
                InitialAmount = 1000m,
                MonthlyContribution = 100m,
                Years = 1,
                AnnualInterestRate = 0m  // 0% de juros anual.
            };

            // Act: executa a simulação.
            var result = service.SimulateInvestment(simulation);

            // Calcula o total investido esperado (1000 inicial + 12 contribuições de 100).
            decimal expectedInvested = 1000m + 100m * 12m;

            // Assert: com juros zero, o montante final deve ser igual ao total investido e juros ganhos zero.
            Assert.Equal(expectedInvested, result.TotalInvested);
            Assert.Equal(expectedInvested, result.FinalAmount);
            Assert.Equal(0m, result.TotalInterestEarned);
        }

        [Fact]
        public void SimulateInvestment_WithInterest_CalculatesInterest()
        {
            // Arrange: configura simulação com taxa de juros positiva.
            var loggerMock = new Mock<ILogger<InvestmentService>>();
            var service = new InvestmentService(loggerMock.Object);
            var simulation = new InvestmentSimulationDto
            {
                InitialAmount = 1000m,
                MonthlyContribution = 0m,
                Years = 2,
                AnnualInterestRate = 10m  // 10% de juros anual.
            };

            // Act: executa a simulação.
            var result = service.SimulateInvestment(simulation);

            // Calcula o montante final esperado aplicando juros mensais composto (10% a.a. por 2 anos, sem contribuições mensais).
            decimal monthlyRate = 0.10m / 12m;
            decimal expectedFinal = 1000m;
            int totalMonths = 2 * 12;
            for (int i = 1; i <= totalMonths; i++)
            {
                expectedFinal *= (1 + monthlyRate);
            }

            // Assert: o montante final deve corresponder (aproximadamente) ao cálculo esperado e ter juros acumulados.
            Assert.InRange(result.FinalAmount, expectedFinal - 0.01m, expectedFinal + 0.01m);
            Assert.Equal(1000m, result.TotalInvested);
            Assert.True(result.TotalInterestEarned > 0m);
        }
    }
}
