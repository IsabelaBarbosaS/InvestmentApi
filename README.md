# Investment API - Simulador de Investimentos

API REST desenvolvida com **.NET 7 (C#)** para gerenciar produtos de investimento e simular rentabilidade com base em taxa de juros. Este projeto foi criado como um **case técnico** para entrevistas, demonstrando arquitetura limpa, uso de boas práticas e potencial de escalabilidade com microserviços.

---

## Funcionalidades

- CRUD de produtos de investimento
- Simulação de retorno com juros compostos
- Documentação interativa com Swagger
- Projeto pronto para containerização com Docker

---

## Endpoints

| Método | Rota                          | Descrição                           |
|--------|-------------------------------|-------------------------------------|
| GET    | `/api/investments`            | Lista todos os produtos             |
| GET    | `/api/investments/{id}`       | Retorna um produto específico       |
| POST   | `/api/investments`            | Cria um novo produto                |
| PUT    | `/api/investments/{id}`       | Atualiza um produto existente       |
| DELETE | `/api/investments/{id}`       | Remove um produto                   |
| POST   | `/api/investments/simulate`   | Simula rentabilidade de um investimento |

Exemplo de payload para simulação:
```json
{
  "initialAmount": 1000,
  "months": 12,
  "annualInterestRate": 10
}
