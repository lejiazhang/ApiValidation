using ClientValidationGenerator.Common.Models;
using FluentValidation.Resources;
using FluentValidation.Validators;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClientValidationGenerator.Common.Factories
{
    public abstract class ValidationRuleCreator
    {
        protected static string GetValidationMessage(IStringSource source)
        {
            return source.ResourceType == null ? source.GetString(null) : null;
        }

        public abstract ValidationRule Create(IPropertyValidator propertyValidator, Type type);
    }
}