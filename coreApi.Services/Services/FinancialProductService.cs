using coreApi.Domain.Models;
using coreApi.Domain.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace coreApi.Services.Services
{
    public class FinancialProductService : IFinancialProductService
    {
        public async Task<IEnumerable<string>> FinancialProductsAsync(FinancialProductQuery query)
        {
            return  new List<string> { "aaaa", "bbbb" };
        }
    }
}
