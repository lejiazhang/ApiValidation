using ClientValidationGenerator.Domain.Models;
using System;
using System.Collections.Generic;
using FluentValidation;
using ClientValidationGenerator.Common.Resolvers;
using ClientValidationGenerator.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;
using System.Net;

namespace ClientValidationGenerator.AspNetCore.Filters
{
    public class ValidationExceptionFilter : ExceptionFilterAttribute
    {
        private readonly ICamelCasePropertyResolver _camelCasePropertyResolver;

        public ValidationExceptionFilter(ICamelCasePropertyResolver camelCasePropertyResolver)
        {
            _camelCasePropertyResolver = camelCasePropertyResolver;
        }

        public override void OnException(ExceptionContext context)
        {
            if (HandleValidationPropertyValidationException(context))
            {
                return;
            }

            if (HandleValidationException(context))
            {
                return;
            }

            HandleFluentValidationException(context);
        }

        private void HandleFluentValidationException(ExceptionContext context)
        {
            var fluentValidationException = context.Exception as ValidationException;
            if (fluentValidationException != null)
            {
                var errors = new List<ValidationErrorModel>();

                foreach (var error in fluentValidationException.Errors)
                {
                    errors.Add(new ValidationErrorModel
                    {
                        Message = error.ErrorMessage,
                        PropertyName = !string.IsNullOrEmpty(error.PropertyName) ? _camelCasePropertyResolver.GetPropertyName(error.PropertyName) : null,
                        ErrorCode = error.ErrorCode
                    });
                }

                CreateResponse(context, errors);
            }
        }

        private bool HandleValidationException(ExceptionContext context)
        {
            var exceptionHandled = false;
            if (context.Exception is System.ComponentModel.DataAnnotations.ValidationException)
            {
                var errors = new List<ValidationErrorModel>
                {
                    new ValidationErrorModel
                    {
                        Message = context.Exception.Message,
                        PropertyName = null
                    }
                };

                CreateResponse(context, errors);
                exceptionHandled = true;
            }

            return exceptionHandled;
        }

        private bool HandleValidationPropertyValidationException(ExceptionContext context)
        {
            var propertyValidationException = context.Exception as PropertyValidationException;

            if (propertyValidationException == null) return false;

            var errors = new List<ValidationErrorModel>();

            if (propertyValidationException.PropertyNames != null && propertyValidationException.PropertyNames.Any())
            {
                foreach (var prop in propertyValidationException.PropertyNames)
                {
                    errors.Add(new ValidationErrorModel
                    {
                        Message = propertyValidationException.Message,
                        PropertyName = !string.IsNullOrEmpty(prop) ? _camelCasePropertyResolver.GetPropertyName(prop) : null,
                        ErrorCode = propertyValidationException.ErrorCode
                    });
                }
            }
            else
            {
                errors.Add(new ValidationErrorModel
                {
                    Message = propertyValidationException.Message,
                    ErrorCode = propertyValidationException.ErrorCode
                });
            }

            CreateResponse(context, errors);

            return true;
        }

        private void CreateResponse(ExceptionContext context, List<ValidationErrorModel> errors)
        {
            var result = new FailedValidationModel { Errors = errors };

            context.Result = new JsonResult(result)
            {
                StatusCode = (int)HttpStatusCode.BadRequest
            };
        }
    }
}
