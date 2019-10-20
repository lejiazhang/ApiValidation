using coreApi.Domain.Utils;
using FluentValidation;
using System;
using System.Collections;
using System.Linq.Expressions;
using System.Text.RegularExpressions;

namespace coreApi.Domain.Validators
{
    public abstract class ModelValidator<TModel> : AbstractValidator<TModel>, IModelValidator<TModel>
         where TModel : class, new()
    {
        private const CascadeMode DefaultCascadeMode = CascadeMode.StopOnFirstFailure;
        private readonly ICustomValidator<TModel> _customValidator;

        protected ModelValidator()
        {
            CascadeMode = DefaultCascadeMode;
        }

        protected ModelValidator(ICustomValidator<TModel> customValidator)
        {
            CascadeMode = DefaultCascadeMode;
            _customValidator = customValidator;
        }

        public virtual void ValidateModel(TModel model)
        {
            if (model == null)
                return;

            this.ValidateAndThrow(model);
            _customValidator?.ValidateModel(model);
        }

        public void ValidateRequired(Expression<Func<TModel, object>> validateExpression)
        {
            string propertyName = PropertyName(validateExpression);
            RuleFor(validateExpression).NotEmpty().WithMessage($"Please specify {propertyName}.");
        }

        public void ValidateUrl(Expression<Func<TModel, string>> validateExpression)
        {
            string propertyName = PropertyName(validateExpression);
            RuleFor(validateExpression)
                .Matches(@"^(http(s)?://)?([\w-]+\.)+[\w-]+(/[\w- ;,./?%&=]*)?$")
                .WithMessage($"Please specify valid url {propertyName}.");
        }

        public void ValidateRequiredForForeignKey(Expression<Func<TModel, int?>> validateExpression)
        {
            string propertyName = PropertyName(validateExpression);
            RuleFor(validateExpression).Must(p => p != null && p.Value > 0).WithMessage($"Please specify {propertyName}.");
        }

        public void ValidateRequiredIfNotNull(Expression<Func<TModel, int?>> validateExpression)
        {
            string propertyName = PropertyName(validateExpression);
            RuleFor(validateExpression).Must(p => p == null || p.Value > 0).WithMessage($"Please specify {propertyName}.");
        }

        public void ValidateRequired(Expression<Func<TModel, int>> validateExpression)
        {
            string propertyName = PropertyName(validateExpression);
            RuleFor(validateExpression).NotEmpty().GreaterThan(0).WithMessage($"Please specify {propertyName}.");
        }

        public void ValidateRequired(Expression<Func<TModel, long>> validateExpression)
        {
            string propertyName = PropertyName(validateExpression);
            RuleFor(validateExpression).NotEmpty().GreaterThan(0).WithMessage($"Please specify {propertyName}.");
        }

        public void ValidateEmail(Expression<Func<TModel, string>> validateExpression)
        {
            var propertyName = PropertyName(validateExpression);
            RuleFor(validateExpression).EmailAddress().WithMessage($"Please specify a valid email address for {propertyName}.");
        }

        private static string PropertyName<TReturn>(Expression<Func<TModel, TReturn>> validateExpression)
        {
            return ExpressionHelpers.GetPropertyName(validateExpression);
        }

        public void ValidateCollection(Expression<Func<TModel, ICollection>> validateExpression, int? minLength, int? maxLength)
        {
            string propertyName = PropertyName(validateExpression);

            var message = $"'{propertyName}' items count must be" + (minLength.HasValue ? $" greater or equal {minLength.Value}" : "") +
                          (maxLength.HasValue ? $" less or equal {maxLength.Value}" : "");

            RuleFor(validateExpression).Must(p => p != null && p.Count >= (minLength ?? 0) && (!maxLength.HasValue || p.Count <= maxLength.Value))
                .WithMessage(message);
        }

        public void ValidateMaxLength(Expression<Func<TModel, string>> validateExpression, int maxLength)
        {
            string propertyName = PropertyName(validateExpression);
            RuleFor(validateExpression).Length(0, maxLength)
                .WithMessage($"'{propertyName}' max length is {maxLength}");
        }

        public void ValidateMatch(Expression<Func<TModel, string>> validateExpression, Regex regex)
        {
            string propertyName = PropertyName(validateExpression);
            RuleFor(validateExpression)
                .Matches(regex)
                .WithMessage($"'{propertyName}' is not in correct format.");
        }

        public void ValidateRange(Expression<Func<TModel, int>> validateExpression, int from, int to)
        {
            RuleFor(validateExpression)
                .GreaterThanOrEqualTo(from)
                .LessThanOrEqualTo(to);
        }
    }

    public interface IModelValidator<in TModel>
    where TModel : class, new()
    {
        void ValidateModel(TModel model);
    }
}