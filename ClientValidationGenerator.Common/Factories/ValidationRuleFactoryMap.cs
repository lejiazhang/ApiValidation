using ClientValidationGenerator.Common.Rules.ComparisonValidator;
using ClientValidationGenerator.Common.Rules.Length;
using ClientValidationGenerator.Common.Rules.Regex;
using ClientValidationGenerator.Common.Rules.Required;
using ClientValidationGenerator.Common.Rules.ValueType.Integer;
using ClientValidationGenerator.Common.Rules.ValueType.Required;
using FluentValidation.Validators;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClientValidationGenerator.Common.Factories
{
    public interface IValidationRuleFactoryMap
    {
        ValidationRuleCreator GetCreator(Type propertyValidatorType);
        IValueValidationRuleCreator GetValueTypeCreator(Type propertyValidatorType);
        bool IsSupportedValidator(Type propertyValidatorType);
        void Set(Type propertyValidatorType, ValidationRuleCreator creator);
    }

    public class ValidationRuleFactoryMap : IValidationRuleFactoryMap
    {
        private readonly IDictionary<Type, ValidationRuleCreator> _rules = new Dictionary<Type, ValidationRuleCreator>()
        {
            { typeof(NotEmptyValidator), new RequiredValidationRuleCreator() },
            { typeof(NotNullValidator), new RequiredValidationRuleCreator() },
            { typeof(LengthValidator), new LengthValidationRuleCreator() },
            { typeof(RegularExpressionValidator), new RegexValidationRuleCreator() },
            { typeof(EmailValidator), new RegexValidationRuleCreator(ignoreCase: true) },
            { typeof(GreaterThanValidator), new ComparisonValidationRuleCreator("greaterThan") },
            { typeof(GreaterThanOrEqualValidator), new ComparisonValidationRuleCreator("greaterThanOrEqual") },
            { typeof(NotEqualValidator), new ComparisonValidationRuleCreator("notEqual") },
            { typeof(EqualValidator), new ComparisonValidationRuleCreator("equal") },
            { typeof(LessThanValidator), new ComparisonValidationRuleCreator("lessThan") },
            { typeof(LessThanOrEqualValidator), new ComparisonValidationRuleCreator("lessThanOrEqual") }
        };

        private readonly IDictionary<Type, IValueValidationRuleCreator> _valueTypeRules = new Dictionary<Type, IValueValidationRuleCreator>()
        {
            { typeof(IntegerValueTypeRegexRuleMatch), new IntegerValueTypeRegexRuleCreator() },
            { typeof(ValueTypeRequiredRuleMatch), new ValueTypeRequiredRuleCreator() }
        };

        public bool IsSupportedValidator(Type propertyValidatorType)
        {
            return _rules.ContainsKey(propertyValidatorType) || _valueTypeRules.ContainsKey(propertyValidatorType);
        }

        public ValidationRuleCreator GetCreator(Type propertyValidatorType)
        {
            return _rules.ContainsKey(propertyValidatorType) ? _rules[propertyValidatorType] : null;
        }

        public IValueValidationRuleCreator GetValueTypeCreator(Type propertyValidatorType)
        {
            return _valueTypeRules.ContainsKey(propertyValidatorType) ? _valueTypeRules[propertyValidatorType] : null;
        }

        public void Set(Type propertyValidatorType, ValidationRuleCreator creator)
        {
            if (_rules.ContainsKey(propertyValidatorType))
            {
                throw new ArgumentException("A rule related with this type already exists");
            }

            _rules.Add(propertyValidatorType, creator);
        }
    }
}
