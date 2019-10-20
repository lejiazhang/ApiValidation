using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClientValidationGenerator.Common.Factories
{
    public interface IValidatorFactory
    {
        IEnumerable<IValidationRule> CreateValidator(Type validatorType);
    }
}
