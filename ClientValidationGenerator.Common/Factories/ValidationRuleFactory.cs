using ClientValidationGenerator.Common.Models;
using FluentValidation.Validators;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClientValidationGenerator.Common.Factories
{
    public interface IValidationRuleFactory
    {
        ValidationRule CreateRule(IPropertyValidator propertyValidator, Type objectType);
        bool IsSupportedValidator(IPropertyValidator propertyValidatorType);
        bool IsChildCollectionValidator(IPropertyValidator propertyValidatorType);
    }

    public class ValidationRuleFactory : IValidationRuleFactory
    {
        private readonly IValidationRuleFactoryMap _validationRuleFactoryMap;

        public ValidationRuleFactory(IValidationRuleFactoryMap validationRuleFactoryMap)
        {
            _validationRuleFactoryMap = validationRuleFactoryMap;
        }

        public bool IsChildCollectionValidator(IPropertyValidator propertyValidatorType)
        {
            return propertyValidatorType is FluentValidation.Validators.ChildValidatorAdaptor;
        }

        public bool IsSupportedValidator(IPropertyValidator propertyValidatorType)
        {
            return _validationRuleFactoryMap.IsSupportedValidator(GetValidatorType(propertyValidatorType));
        }

        public ValidationRule CreateRule(IPropertyValidator propertyValidator, Type objectType)
        {
            var ruleCreator = _validationRuleFactoryMap.GetCreator(GetValidatorType(propertyValidator));

            return ruleCreator?.Create(propertyValidator, objectType);
        }

        public ValidationRule CreateValueTypeRule(ValueTypeRuleMatch valueTypeRuleMatch)
        {
            var valueTypeRuleCreator = _validationRuleFactoryMap.GetValueTypeCreator(valueTypeRuleMatch.GetType());

            return valueTypeRuleCreator?.Create(valueTypeRuleMatch);
        }

        private Type GetValidatorType(IPropertyValidator propertyValidator)
        {
            var delegatinValidator = propertyValidator as DelegatingValidator;

            if (delegatinValidator != null)
            {
                return GetValidatorType(delegatinValidator.InnerValidator);
            }

            return propertyValidator.GetType();
        }
    }
}
