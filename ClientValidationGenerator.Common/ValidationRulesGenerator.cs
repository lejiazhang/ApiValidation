using ClientValidationGenerator.Common.Extensions;
using ClientValidationGenerator.Common.Factories;
using ClientValidationGenerator.Common.Models;
using ClientValidationGenerator.Common.Resolvers;
using FluentValidation.Internal;
using FluentValidation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ClientValidationGenerator.Common
{

    public interface IValidationRulesGenerator
    {
        IDictionary<string, ValidationModel> Generate();
    }

    public class ValidationRulesGenerator : IValidationRulesGenerator
    {
        private readonly IValidatorFactory _validatorFactory;
        private readonly IValidationRuleFactory _validationRuleFactory;
        private readonly ICamelCasePropertyResolver _propertyResolver;
        private readonly IValueTypeRuleFactory _valueTypeRuleFactory;
        private readonly IValueTypeMatches _valueTypeMatchesFinder;

        public ValidationRulesGenerator(IValidatorFactory validatorFactory, IValidationRuleFactory validationRuleFactory, ICamelCasePropertyResolver propertyResolver, IValueTypeRuleFactory valueTypeRuleFactory, IValueTypeMatches valueTypeMatchesFinder)
        {
            _validatorFactory = validatorFactory;
            _validationRuleFactory = validationRuleFactory;
            _propertyResolver = propertyResolver;
            _valueTypeRuleFactory = valueTypeRuleFactory;
            _valueTypeMatchesFinder = valueTypeMatchesFinder;
        }

        public IDictionary<string, ValidationModel> Generate()
        {
            var toReturn = new Dictionary<string, ValidationModel>();

            var modelValidators = ValidatorsResolver.GetAll();

            foreach (var modelValidator in modelValidators)
            {
                var model = new ValidationModel();
                var modelType = modelValidator.GetModelType();
                var modelName = _propertyResolver.GetModelName(modelType);

                if (toReturn.ContainsKey(modelName))
                    continue;

                GenerateRules(model, modelType);

                toReturn.Add(modelName, model);
            }

            return toReturn;
        }

        private void GenerateRules(ValidationModel model, Type propertyType, string prefix = "")
        {
            var validatorsForProperty = ValidatorsResolver.GetAllForType(propertyType);

            if (!validatorsForProperty.Any())
                return;

            foreach (var validatorForProperty in validatorsForProperty)
            {

                GenerateChildRules(model, validatorForProperty, prefix);
            }

            foreach (var nestedPropertyInfo in propertyType.GetProperties())
            {
                if (nestedPropertyInfo.PropertyType.IsValueType)
                {
                    GenerateValueTypesRules(model, nestedPropertyInfo, prefix);
                }
                else if (nestedPropertyInfo.PropertyType.IsGenericType)
                {
                    var genericType = nestedPropertyInfo.PropertyType.GetGenericArguments().First();
                    GenerateRules(model, genericType, nestedPropertyInfo.Name.ToPrefix(prefix));
                }else if (nestedPropertyInfo.PropertyType.IsClass)
                {
                    GenerateRules(model, nestedPropertyInfo.PropertyType, nestedPropertyInfo.Name.ToPrefix(prefix));
                }
            }
        }


        private void GenerateValueTypesRules(ValidationModel model, PropertyInfo nestedPropertyInfo, string prefix)
        {
            var valueTypeRuleMatches = _valueTypeMatchesFinder.GetMatches(nestedPropertyInfo.PropertyType);

            if (!valueTypeRuleMatches.Any())
                return;

            var property = _propertyResolver.GetPropertyName(prefix + nestedPropertyInfo.Name);
            var field = model.GetOrCreateValidationProperty(property);

            foreach (var valueTypeRuleMatch in valueTypeRuleMatches)
            {
                var validationRule = _valueTypeRuleFactory.CreateRule(valueTypeRuleMatch);
                if (validationRule == null)
                    continue;
                if (!field.Rules.ContainsKey(validationRule.Name))
                    field.Rules.Add(validationRule.Name, validationRule);
            }
        }

        private void GenerateSingleRule(ValidationProperty field, IPropertyValidator propertyValidator, Type propertyType, string prefix)
        {
            var validationRule = _validationRuleFactory.CreateRule(propertyValidator, propertyType);

            if (validationRule == null)
                return;

            if (field.Rules.ContainsKey(validationRule.Name))
            {
                if (string.IsNullOrWhiteSpace(prefix))
                {
                    field.Rules[validationRule.Name] = validationRule;
                }
            }
            else
            {
                field.Rules.Add(validationRule.Name, validationRule);
            }
        }

        private void GenerateChildRules(ValidationModel model, Type validatorType, string prefix)
        {
            var validators = _validatorFactory.CreateValidator(validatorType);

            if (validators == null)
                return;

            foreach (var validatorRule in validators)
            {
                var propertyRule = validatorRule as PropertyRule;
                if (propertyRule?.PropertyName == null)
                {
                    continue;
                }

                var property = _propertyResolver.GetPropertyName(prefix + propertyRule.PropertyName);
                var field = model.GetOrCreateValidationProperty(property);

                foreach (var propertyValidator in propertyRule.Validators)
                {
                    if (_validationRuleFactory.IsSupportedValidator(propertyValidator))
                    {
                        GenerateSingleRule(field, propertyValidator, propertyRule.Member.DeclaringType, prefix);
                    }
                    else if (_validationRuleFactory.IsChildCollectionValidator(propertyValidator))
                    {
                        var childCollectionValidator = propertyValidator as ChildValidatorAdaptor;
                        if (childCollectionValidator == null)
                            return;

                        GenerateChildRules(model, childCollectionValidator.ValidatorType, property.ToPrefix());
                    }
                }
            }

        }
    }
}
