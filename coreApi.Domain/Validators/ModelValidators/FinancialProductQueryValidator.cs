using coreApi.Domain.Models;

namespace coreApi.Domain.Validators.ModelValidators
{
    public class FinancialProductQueryValidator : ModelValidator<FinancialProductQuery>
    {
        public FinancialProductQueryValidator()
        {
            ValidateRequired(p => p.AssetModelId);
            ValidateRequired(p => p.CompanyId);
            ValidateRequired(p => p.ModelYear);
            ValidateRequired(p => p.NST);
            ValidateRequired(p => p.PMSDealerNbr);
        }
    }
}
