using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClientValidationGenerator.Common.Resolvers
{
    public interface ICamelCasePropertyResolver
    {
        string GetPropertyName(string property);
        string GetModelName(Type modelType);
    }


    public class CamelCasePropertyResolver : ICamelCasePropertyResolver
    {
        private readonly CamelCasePropertyNamesContractResolver _propertyResolver;

        public CamelCasePropertyResolver()
        {
            _propertyResolver = new CamelCasePropertyNamesContractResolver();
        }

        public string GetModelName(Type modelType)
        {
            return _propertyResolver.GetResolvedPropertyName(modelType.Name);
        }

        public string GetPropertyName(string property)
        {
            if (string.IsNullOrEmpty(property))
                return property;

            if (property.Contains("."))
            {
                return GetSplittedPropertyName(property);
            }

            return _propertyResolver.GetResolvedPropertyName(property);
        }

        private string GetSplittedPropertyName(string property)
        {
            var splitted = property.Split('.')
                .Select(value => _propertyResolver.GetResolvedPropertyName(value));

            return String.Join(".", splitted);
        }
    }
}
