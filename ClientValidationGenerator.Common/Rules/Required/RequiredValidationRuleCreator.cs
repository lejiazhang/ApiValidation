using ClientValidationGenerator.Common.Factories;
using ClientValidationGenerator.Common.Models;
using FluentValidation.Validators;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClientValidationGenerator.Common.Rules.Required
{
    public class RequiredValidationRuleCreator : ValidationRuleCreator
    {
        public override ValidationRule Create(IPropertyValidator propertyValidator, Type objectType)
        {
            return new RequiredValidationRule(GetValidationMessage(propertyValidator.Options.ErrorMessageSource));
        }
    }
}