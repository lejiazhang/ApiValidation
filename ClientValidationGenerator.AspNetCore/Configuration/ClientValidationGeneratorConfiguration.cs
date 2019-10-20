using ClientValidationGenerator.AspNetCore.Factories;
using ClientValidationGenerator.AspNetCore.Filters;
using ClientValidationGenerator.Common;
using ClientValidationGenerator.Common.Factories;
using ClientValidationGenerator.Common.Models;
using ClientValidationGenerator.Common.Resolvers;
using ClientValidationGenerator.Common.Rules.ValueType.Integer;
using ClientValidationGenerator.Common.Rules.ValueType.Required;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
namespace ClientValidationGenerator.AspNetCore.Configuration
{
    public static class ClientValidationGeneratorConfiguration
    {
        public static void AddClientValidationGenerator(this IServiceCollection services)
        {
            services.AddTransient<IValidationRuleFactory, ValidationRuleFactory>();
            services.AddTransient<IValidationRuleFactoryMap, ValidationRuleFactoryMap>();
            services.AddTransient<IValidatorFactory, ValidatorFactory>();
            services.AddTransient<IValidationRulesGenerator, ValidationRulesGenerator>();
            services.AddTransient<ICamelCasePropertyResolver, CamelCasePropertyResolver>();
            services.AddTransient<IValueTypeRuleFactory, ValueTypeRuleFactory>();
            services.AddTransient<IValueTypeMatches, ValueTypeMatchesFinder>();
            services.AddTransient<ValueTypeRuleMatch, IntegerValueTypeRegexRuleMatch>();
            services.AddTransient<ValueTypeRuleMatch, ValueTypeRequiredRuleMatch>();
            services.AddTransient<IList<ValueTypeRuleMatch>>(provider => provider.GetServices<ValueTypeRuleMatch>().ToList());
        }

        public static void AddValidationExceptionFilter(this FilterCollection filters)
        {
            filters.Add<ValidationExceptionFilter>();
        }
    }
}
