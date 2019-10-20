using System;
using System.Collections.Generic;
using System.Text;

namespace ClientValidationGenerator.Domain.Models
{
    public class ValidationErrorModel
    {
        public string PropertyName { get; set; }

        public string Message { get; set; }

        public string ErrorCode { get; set; }
    }
}
