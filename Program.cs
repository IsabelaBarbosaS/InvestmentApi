using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using InvestmentApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Configurações de serviços (Dependency Injection).
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();  // Adiciona Swagger para documentação da API.
builder.Services.AddSingleton<IInvestmentService, InvestmentService>();  // Injeta o serviço de investimento como singleton.

// Constrói o aplicativo.
var app = builder.Build();

// Configura o pipeline de requisição HTTP.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

// Inicia a aplicação web.
app.Run();
