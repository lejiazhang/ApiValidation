using FluentValidation;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClientValidationGenerator.Common.Extensions
{
    public static class TypeExtensions
    {
        public static Type GetModelType(this Type validatorType)
        {
            var iValidator = validatorType.GetInterfaces().SingleOrDefault(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IValidator<>));

            return iValidator?.GenericTypeArguments.SingleOrDefault();
        }

        public static bool IsNullable(this Type type)
        {
            return Nullable.GetUnderlyingType(type) != null;
        }

        public static bool IsEnumerable(this Type type)
        {
            return typeof(IEnumerable).IsAssignableFrom(type);
        }
    }
}
