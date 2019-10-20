using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClientValidationGenerator.Common.Models
{
    public class ValidationProperty
    {
        public ValidationProperty()
        {
            Rules = new Dictionary<string, ValidationRule>();
        }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Placeholder { get; set; }

        public IDictionary<string, ValidationRule> Rules { get; set; }
    }
}
