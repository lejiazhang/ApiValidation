using ClientValidationGenerator.Common.Extensions;
using ClientValidationGenerator.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClientValidationGenerator.Common.Rules.ValueType.Required
{
    public class ValueTypeRequiredRuleMatch : ValueTypeRuleMatch
    {
        public override bool IsSupported(Type fieldType)
        {
            return fieldType.IsValueType && !fieldType.IsNullable();
        }

        public ValueTypeRequiredRuleMatch() : base("This field is required") { }
    }
}
