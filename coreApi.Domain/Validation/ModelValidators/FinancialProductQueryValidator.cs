using coreApi.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace coreApi.Domain.Validation.ModelValidators
{
    public class FinancialProductQueryValidator : ModelValidator<FinancialProductQuery>
    {
        public FinancialProductQueryValidator()
        {
            ValidateRequired(p => p.AssetModelId);
            ValidateRange(p => p.AssetModelId, 0, 9999);

            ValidateRequired(p => p.CompanyId);
            ValidateRange(p => p.AssetModelId, 0, 1);

            ValidateRequired(p => p.ModelYear);
            ValidateRange(p => p.AssetModelId, 1, 4);

            ValidateRequired(p => p.NST);
            ValidateMaxLength(p => p.NST, 10);

            ValidateRequired(p => p.PMSDealerNbr);
            ValidateMaxLength(p => p.PMSDealerNbr, 5);
        }
    }
}
