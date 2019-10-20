using coreApi.Domain.Models;

namespace coreApi.Domain.Validators.ModelValidators
{
    public class FinancialProductQueryValidator : ModelValidator<FinancialProductQuery>
    {
        public FinancialProductQueryValidator()
        {
            ValidateRequired(p => p.AssetModelId);
        }
    }
}
