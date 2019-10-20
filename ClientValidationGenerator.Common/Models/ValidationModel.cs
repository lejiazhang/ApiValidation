using System;
using System.Collections.Generic;
using System.Text;

namespace ClientValidationGenerator.Common.Models
{
    public class ValidationModel
    {

        public ValidationProperty GetOrCreateValidationProperty(string propertyName)
        {
            if (Fields.ContainsKey(propertyName))
                return Fields[propertyName];

            var property = new ValidationProperty();
            Fields.Add(propertyName, property);

            return property;
        }

        public ValidationModel()
        {
            Fields = new Dictionary<string, ValidationProperty>();
        }

        public IDictionary<string, ValidationProperty> Fields { get; set; }
    }
}
