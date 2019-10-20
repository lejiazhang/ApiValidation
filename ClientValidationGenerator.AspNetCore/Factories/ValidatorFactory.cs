using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using IValidatorFactory = ClientValidationGenerator.Common.Factories.IValidatorFactory;

namespace ClientValidationGenerator.AspNetCore.Factories
{
    public class ValidatorFactory : IValidatorFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public ValidatorFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IEnumerable<IValidationRule> CreateValidator(Type validatorType)
        {
            return _serviceProvider.GetService(validatorType) as IEnumerable<IValidationRule>;
        }
    }
}
