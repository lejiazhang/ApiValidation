using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using ClientValidationGenerator.Common;
using ClientValidationGenerator.Common.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClientValidationGenerator.AspNetCore.Controllers
{
    [AllowAnonymous]
    public class ValidationController : ControllerBase
    {
        private readonly IValidationRulesGenerator _validationRulesGenerator;

        public ValidationController(IValidationRulesGenerator validationRulesGenerator)
        {
            _validationRulesGenerator = validationRulesGenerator;
        }

        [ProducesResponseType(typeof(IDictionary<string, ValidationModel>), (int)HttpStatusCode.OK)]
        [Route("api/validation")]
        [HttpGet]
        public IActionResult GetValidations()
        {
            return Ok(_validationRulesGenerator.Generate());
        }
    }
}
