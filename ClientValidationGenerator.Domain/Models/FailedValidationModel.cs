using System;
using System.Collections.Generic;
using System.Text;

namespace ClientValidationGenerator.Domain.Models
{
    public class FailedValidationModel
    {
        public IEnumerable<ValidationErrorModel> Errors { get; set; }
    }
}
