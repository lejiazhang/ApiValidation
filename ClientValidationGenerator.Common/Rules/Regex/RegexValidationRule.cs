using ClientValidationGenerator.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClientValidationGenerator.Common.Rules.Regex
{
    public class RegexValidationRule : ValidationRule
    {
        public const string RegexValidationRuleName = "regex";
        public RegexValidationRule(string regex, bool ignoreCase, string message) : base(RegexValidationRuleName, message)
        {
            Regex = regex;
            IgnoreCase = ignoreCase;
        }

        public string Regex { get; set; }
        public bool IgnoreCase { get; set; }
    }
}