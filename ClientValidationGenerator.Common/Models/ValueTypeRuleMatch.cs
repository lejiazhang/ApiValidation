using System;
using System.Collections.Generic;
using System.Text;

namespace ClientValidationGenerator.Common.Models
{
    public class ValueTypeRuleMatch
    {
        private readonly IList<Type> _supportedTypes;
        protected ValueTypeRuleMatch(string validationMessage, params Type[] supportedTypes)
        {
            ValidationMessage = validationMessage;
            _supportedTypes = supportedTypes;
        }

        public virtual bool IsSupported(Type fieldType)
        {
            return _supportedTypes != null && _supportedTypes.Contains(fieldType);
        }

        public string ValidationMessage { get; set; }
    }
}
