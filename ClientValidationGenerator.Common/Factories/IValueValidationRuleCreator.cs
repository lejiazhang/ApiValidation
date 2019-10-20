using ClientValidationGenerator.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClientValidationGenerator.Common.Factories
{
    public interface IValueValidationRuleCreator
    {
        ValidationRule Create(ValueTypeRuleMatch match);
    }
}