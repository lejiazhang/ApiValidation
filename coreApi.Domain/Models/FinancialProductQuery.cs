using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace coreApi.Domain.Models
{
    public class FinancialProductQuery
    {
        public int AssetModelId { get; set; }
        public string NST { get; set; }

        public int ModelYear { get; set; }
        public int CompanyId { get; set; }
        public string PMSDealerNbr { get; set; }
    }
}
