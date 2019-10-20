using ClientValidationGenerator.Common.Factories;
using ClientValidationGenerator.Common.Models;
using FluentValidation.Validators;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClientValidationGenerator.Common.Rules.Regex
{
    public class RegexValidationRuleCreator : ValidationRuleCreator
    {
        private readonly bool _ignoreCase;

        public RegexValidationRuleCreator(bool ignoreCase = false)
        {
            _ignoreCase = ignoreCase;
        }

        public override ValidationRule Create(IPropertyValidator propertyValidator, Type objectType)
        {
            var regexValidator = propertyValidator as IRegularExpressionValidator;
            if (regexValidator == null)
                return null;

            return new RegexValidationRule(regexValidator.Expression, _ignoreCase, GetValidationMessage(propertyValidator.Options.ErrorMessageSource));
        }
    }
}