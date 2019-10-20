using ClientValidationGenerator.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClientValidationGenerator.Common.Rules.Required
{
    public class RequiredValidationRule : ValidationRule
    {
        public const string RequiredValidationRuleName = "required";

        public RequiredValidationRule(string message) : base(RequiredValidationRuleName, message)
        {
            Required = true;
        }

        public bool Required { get; set; }
    }
}
