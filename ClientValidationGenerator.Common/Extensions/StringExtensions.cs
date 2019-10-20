using System;
using System.Collections.Generic;
using System.Text;

namespace ClientValidationGenerator.Common.Extensions
{
    public static class StringExtensions
    {
        public static string ToPrefix(this string property, string prefix = "")
        {
            return prefix + property + ".";
        }
    }
}
