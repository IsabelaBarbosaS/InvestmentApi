using InvestmentApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace InvestmentApi.Services
{
    public class InvestmentService
    {
        private readonly List<Investment> _investments = new();

        public IEnumerable<Investment> GetAll() => _investments;

        public Investment GetById(Guid id) =>
            _investments.FirstOrDefault(i => i.Id == id);

        public void Add(Investment investment)
        {
            investment.Id = Guid.NewGuid();
            _investments.Add(investment);
        }

        public void Update(Guid id, Investment updated)
        {
            var existing = GetById(id);
            if (existing != null)
            {
                existing.Name = updated.Name;
                existing.Type = updated.Type;
                existing.InterestRate = updated.InterestRate;
            }
        }

        public void Delete(Guid id)
        {
            var investment = GetById(id);
            if (investment != null)
            {
                _investments.Remove(investment);
            }
        }

        public decimal SimulateReturn(decimal initialAmount, int months, decimal annualRate)
        {
            double rate = (double)annualRate / 100 / 12;
            double amount = (double)initialAmount * Math.Pow(1 + rate, months);
            return (decimal)amount;
        }
    }
}
