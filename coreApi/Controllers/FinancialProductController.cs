using System.Threading.Tasks;
using coreApi.Domain.Models;
using coreApi.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace coreApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FinancialProductController : ControllerBase
    {
        private readonly IFinancialProductService _financialProductService;
        public FinancialProductController(IFinancialProductService financialProductService)
        {
            _financialProductService = financialProductService;
        }

        [HttpGet]
        public async Task<IActionResult> FinancialProducts(FinancialProductQuery query)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(await _financialProductService.FinancialProductsAsync(query));
        }

    }
}