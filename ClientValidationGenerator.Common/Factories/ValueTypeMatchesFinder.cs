using ClientValidationGenerator.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClientValidationGenerator.Common.Factories
{
    public class ValueTypeMatchesFinder : IValueTypeMatches
    {
        private readonly IList<ValueTypeRuleMatch> _valueTypeRules;
        public ValueTypeMatchesFinder(IList<ValueTypeRuleMatch> valueTypeRules)
        {
            _valueTypeRules = valueTypeRules;
        }

        public IEnumerable<ValueTypeRuleMatch> GetMatches(Type propertyType)
        {
            if (!propertyType.IsValueType)
                return Enumerable.Empty<ValueTypeRuleMatch>();

            return _valueTypeRules.Where(x => x.IsSupported(propertyType)).ToList();
        }
    }

    public interface IValueTypeMatches
    {
        IEnumerable<ValueTypeRuleMatch> GetMatches(Type propertyType);

    }

}
