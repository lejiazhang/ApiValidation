using coreApi.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace coreApi.Domain.Services
{
    public interface IFinancialProductService
    {
        Task<IEnumerable<string>> FinancialProductsAsync(FinancialProductQuery query);
    }
}
