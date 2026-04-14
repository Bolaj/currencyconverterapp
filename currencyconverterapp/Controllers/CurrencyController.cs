using currencyconverterapp.Models.Dtos;
using currencyconverterapp.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace currencyconverterapp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurrencyController : ControllerBase
    {
        private readonly ICurrency currencyService;
        public CurrencyController(ICurrency currencyService)
        {
            this.currencyService = currencyService;
        }

        [HttpPost("convert")]
        public async Task<IActionResult> Convert([FromBody] CurrencyConvertRequestDto request)
        {
            var result = await currencyService.Convert(
                request.From,
                request.To,
                request.Amount
            );

            return Ok(new
            {
                request.From,
                request.To,
                request.Amount,
                convertedAmount = result
            });
        }
    }
}