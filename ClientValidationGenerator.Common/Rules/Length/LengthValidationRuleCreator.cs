using ClientValidationGenerator.Common.Factories;
using ClientValidationGenerator.Common.Models;
using FluentValidation.Validators;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClientValidationGenerator.Common.Rules.Length
{
    public class LengthValidationRuleCreator : ValidationRuleCreator
    {
        public override ValidationRule Create(IPropertyValidator propertyValidator, Type objectType)
        {
            var lengthValidator = propertyValidator as LengthValidator;

            if (lengthValidator == null)
                return null;

            return new LengthValidationRule(lengthValidator.Min, lengthValidator.Max, GetValidationMessage(propertyValidator.Options.ErrorMessageSource));
        }
    }
}
