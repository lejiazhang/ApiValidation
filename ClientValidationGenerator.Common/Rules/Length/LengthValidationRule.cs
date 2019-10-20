using ClientValidationGenerator.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClientValidationGenerator.Common.Rules.Length
{
    public class LengthValidationRule : ValidationRule
    {
        public LengthValidationRule(int min, int max, string message) : base("length", message)
        {
            Min = min;
            Max = max;
        }

        public int Min { get; set; }
        public int Max { get; set; }
    }
}
