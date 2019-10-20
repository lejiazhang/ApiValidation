using ClientValidationGenerator.Common.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClientValidationGenerator.Common.Rules.ComparisonValidator
{
    public class ComparisonValidationRule : ValidationRule
    {
        private static string ToCamelCase(string property)
        {
            return property.Substring(0, 1).ToLowerInvariant() + property.Substring(1);
        }

        public ComparisonValidationRule(string property, string name, string message) : base(name, message)
        {
            Property = ToCamelCase(property);
        }
        public ComparisonValidationRule(object value, string name, string message) : base(name, message)
        {
            Value = value;
        }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Property { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public object Value { get; set; }
    }
}
