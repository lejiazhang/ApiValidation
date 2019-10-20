using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace ClientValidationGenerator.Common.Resolvers
{

#if DEBUG
    public static class Validators
    {
        internal static readonly IList<Type> Types = new List<Type>();
        public static void Register(Type type)
        {
            Types.Add(type);
        }
    }
#endif

    internal class ValidatorsResolver
    {
        internal static IList<Type> GetAllForType(Type type)
        {

#if DEBUG
            var types = GetTypesFromAssemblies();
#else
			var types = GetTypesFromBCAssemblies();
#endif
            return types
                   .Where(t => !t.IsAbstract && !t.IsGenericTypeDefinition && t.IsClass && t.BaseType != null &&
                               t.BaseType.IsGenericType &&
                               t.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IValidator<>) && i.GenericTypeArguments.Contains(type)))
                   .ToList();
        }

        internal static IList<Type> GetAll()
        {
#if DEBUG
            return GetAll(GetTypesFromAssemblies());
#else
			return GetAll(GetTypesFromBCAssemblies());
#endif
        }

        internal static IList<Type> GetAll(Assembly assembly)
        {
            return GetAll(assembly.GetTypes());
        }

        internal static IList<Type> GetAll(IEnumerable<Type> types)
        {
            return types
                .Where(t => !t.IsAbstract && !t.IsGenericTypeDefinition && t.IsClass && t.BaseType != null &&
                            t.BaseType.IsGenericType &&
                            t.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IValidator<>)))
                .ToList();
        }

        private static IList<Type> GetTypesFromAssemblies()
        {
            return AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(a => a.GetTypes())
                .ToList();
        }
    }
}
