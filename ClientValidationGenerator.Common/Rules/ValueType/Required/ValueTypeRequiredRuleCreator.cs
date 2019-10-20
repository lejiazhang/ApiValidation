using ClientValidationGenerator.Common.Factories;
using ClientValidationGenerator.Common.Models;
using ClientValidationGenerator.Common.Rules.Required;

namespace ClientValidationGenerator.Common.Rules.ValueType.Required
{
    public class ValueTypeRequiredRuleCreator : IValueValidationRuleCreator
    {
        public ValidationRule Create(ValueTypeRuleMatch match)
        {
            var requiredMatcher = match as ValueTypeRequiredRuleMatch;
            if (requiredMatcher == null)
                return null;

            return new RequiredValidationRule(requiredMatcher.ValidationMessage);
        }
    }
}