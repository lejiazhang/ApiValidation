using ClientValidationGenerator.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClientValidationGenerator.Common.Factories
{
    public interface IValueTypeRuleFactory
    {
        ValidationRule CreateRule(ValueTypeRuleMatch match);
    }

    public class ValueTypeRuleFactory : IValueTypeRuleFactory
    {
        private readonly IValidationRuleFactoryMap _validationRuleFactoryMap;

        public ValueTypeRuleFactory(IValidationRuleFactoryMap validationRuleFactoryMap)
        {
            _validationRuleFactoryMap = validationRuleFactoryMap;
        }

        public ValidationRule CreateRule(ValueTypeRuleMatch match)
        {
            var ruleCreator = _validationRuleFactoryMap.GetValueTypeCreator(match.GetType());

            return ruleCreator?.Create(match);
        }
    }
}
