using ClientValidationGenerator.Common.Factories;
using ClientValidationGenerator.Common.Models;
using FluentValidation.Validators;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace ClientValidationGenerator.Common.Rules.ComparisonValidator
{
    public class ComparisonValidationRuleCreator : ValidationRuleCreator
    {
        private readonly string _ruleName;

        private static string GetPropertyName(MemberInfo property, Type objectType)
        {
            if (property.DeclaringType == null || property.DeclaringType == objectType)
            {
                return property.Name;
            }
            else
            {
                return $"{property.DeclaringType.Name}.{property.Name}";
            }
        }

        public ComparisonValidationRuleCreator(string ruleName)
        {
            _ruleName = ruleName;
        }

        public override ValidationRule Create(IPropertyValidator propertyValidator, Type objectType)
        {
            var comparisonValidator = propertyValidator as IComparisonValidator;

            if (comparisonValidator == null)
            {
                return null;
            }

            if (comparisonValidator.ValueToCompare != null)
            {
                return new ComparisonValidationRule(comparisonValidator.ValueToCompare, _ruleName, GetValidationMessage(comparisonValidator.Options.ErrorMessageSource));
            }

            if (comparisonValidator.MemberToCompare != null && comparisonValidator.MemberToCompare.MemberType == MemberTypes.Property)
            {

                return new ComparisonValidationRule(GetPropertyName(comparisonValidator.MemberToCompare, objectType), _ruleName, GetValidationMessage(comparisonValidator.Options.ErrorMessageSource));
            }

            return null;
        }
    }
}