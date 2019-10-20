using System;
using System.Collections.Generic;
using System.Text;

namespace ClientValidationGenerator.Domain.Exceptions
{
    public class PropertyValidationException : Exception
    {
        public PropertyValidationException(string message) : base(message)
        {
        }

        public PropertyValidationException(string message, string propertyName) : base(message)
        {
            PropertyNames = new[] { propertyName };
        }

        public PropertyValidationException(string message, string[] propertyNames) : base(message)
        {
            PropertyNames = propertyNames;
        }

        public PropertyValidationException(string message, string propertyName, string errorCode) : base(message)
        {
            PropertyNames = new[] { propertyName };
            ErrorCode = errorCode;
        }

        public PropertyValidationException(string message, string[] propertyNames, string errorCode) : base(message)
        {
            PropertyNames = propertyNames;
            ErrorCode = errorCode;
        }


        public string[] PropertyNames { get; set; }

        public string ErrorCode { get; set; }
    }
}
