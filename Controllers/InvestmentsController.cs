using Microsoft.AspNetCore.Mvc;
using InvestmentApi.Models;
using InvestmentApi.Services;
using InvestmentApi.DTOs;
using System;

namespace InvestmentApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InvestmentsController : ControllerBase
    {
        private readonly InvestmentService _service;

        public InvestmentsController(InvestmentService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetAll() => Ok(_service.GetAll());

        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            var investment = _service.GetById(id);
            return investment == null ? NotFound() : Ok(investment);
        }

        [HttpPost]
        public IActionResult Create(Investment investment)
        {
            _service.Add(investment);
            return CreatedAtAction(nameof(GetById), new { id = investment.Id }, investment);
        }

        [HttpPut("{id}")]
        public IActionResult Update(Guid id, Investment updated)
        {
            _service.Update(id, updated);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            _service.Delete(id);
            return NoContent();
        }

        [HttpPost("simulate")]
        public IActionResult Simulate(SimulationRequest request)
        {
            var finalAmount = _service.SimulateReturn(request.InitialAmount, request.Months, request.AnnualInterestRate);
            return Ok(new SimulationResult { FinalAmount = finalAmount });
        }
    }
}
