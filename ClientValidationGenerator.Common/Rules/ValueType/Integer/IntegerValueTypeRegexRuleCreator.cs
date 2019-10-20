using ClientValidationGenerator.Common.Factories;
using ClientValidationGenerator.Common.Models;
using ClientValidationGenerator.Common.Rules.Regex;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClientValidationGenerator.Common.Rules.ValueType.Integer
{
    public class IntegerValueTypeRegexRuleCreator : IValueValidationRuleCreator
    {
        public ValidationRule Create(ValueTypeRuleMatch match)
        {
            var integerValueMatcher = match as IntegerValueTypeRegexRuleMatch;
            if (integerValueMatcher == null)
                return null;

            return new RegexValidationRule(integerValueMatcher.Regex, false, integerValueMatcher.ValidationMessage);
        }
    }
}