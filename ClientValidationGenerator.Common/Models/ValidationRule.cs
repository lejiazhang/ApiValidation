using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClientValidationGenerator.Common.Models
{
    public abstract class ValidationRule
    {
        protected ValidationRule(string name, string message)
        {
            Name = name;
            Message = message;
        }

        [JsonIgnore]
        public string Name { get; private set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Message { get; set; }
    }
}
