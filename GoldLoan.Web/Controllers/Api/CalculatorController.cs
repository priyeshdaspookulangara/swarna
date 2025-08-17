using GoldLoan.Application.DTOs;
using GoldLoan.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GoldLoan.Web.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalculatorController : ControllerBase
    {
        private readonly IFinancialCalculatorService _calculatorService;

        public CalculatorController(IFinancialCalculatorService calculatorService)
        {
            _calculatorService = calculatorService;
        }

        [HttpPost("calculate")]
        public IActionResult Calculate([FromBody] CalculationRequestDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var emi = _calculatorService.CalculateEmi(request.Principal, request.AnnualRate, request.TenureInMonths);
            var bullet = _calculatorService.CalculateBulletRepaymentAmount(request.Principal, request.AnnualRate, request.TenureInMonths);

            var response = new CalculationResponseDto
            {
                EmiAmount = emi,
                BulletRepaymentAmount = bullet
            };

            return Ok(response);
        }
    }
}
