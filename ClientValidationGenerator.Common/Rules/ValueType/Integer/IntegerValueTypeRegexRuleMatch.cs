using ClientValidationGenerator.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClientValidationGenerator.Common.Rules.ValueType.Integer
{
    public class IntegerValueTypeRegexRuleMatch : ValueTypeRuleMatch
    {
        public IntegerValueTypeRegexRuleMatch() : base("Field value must be integer", typeof(int), typeof(long), typeof(byte), typeof(int?), typeof(long?), typeof(byte?))
        {
        }

        public string Regex => @"^([-]?[1-9]\d*|0)$";
    }
}
