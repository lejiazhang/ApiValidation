using ClientValidationGenerator.Domain.Exceptions;
using coreApi.Domain.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace coreApi.Domain.Validation
{
    public abstract class CustomValidator<T> : ICustomValidator<T>
         where T : class, new()
    {
        public void ValidateModel(T model)
        {
            ValidateCore(model);
        }

        protected abstract void ValidateCore(T model);

        protected void ValidateUnique(
            string propertyValue,
            Expression<Func<T, bool>> anyExpression,
            Func<Expression<Func<T, bool>>, bool> anyCheckFunc)
        {
            ValidateUnique<T>(propertyValue, anyExpression, anyCheckFunc);
        }

        protected void ValidateUnique<TModel>(
            string propertyValue,
            Expression<Func<TModel, bool>> anyExpression,
            Func<Expression<Func<TModel, bool>>, bool> anyCheckFunc)
            where TModel : class, new()
        {
            var targetExpression = anyExpression;
           
            if (anyCheckFunc(targetExpression))
            {
                string typeName = typeof(T).Name;
                string propertyName = ((MemberExpression)((BinaryExpression)anyExpression.Body).Left).Member.Name;
                throw new PropertyValidationException(
                    $"{typeName} cannot be saved. {typeName} with {propertyName}: '{propertyValue}' already exists.",
                    propertyName);
            }
        }

        protected void ValidateDictionaryExists(Expression<Func<T, object>> propertyToValidate, Func<bool> exists)
        {
            string propertyName = ExpressionHelpers.GetPropertyName(propertyToValidate);
            bool exist = exists();
            if (!exist)
            {
                throw new PropertyValidationException($"{propertyName} value is not allowed", propertyName);
            }
        }

        protected void ValidateContainsCollection<Model>(
            Expression<Func<T, object>> propertyToValidate,
            IEnumerable<Model> containedIn,
            IEnumerable<Model> compared)
        {
            string propertyName = ExpressionHelpers.GetPropertyName(propertyToValidate);

            if (compared == null || compared.Any() == false)
                return;

            if (containedIn == null)
                containedIn = new List<Model>();

            var allContains = compared.All(p => containedIn.Any(c => p.Equals(c)));
            if (!allContains)
            {
                throw new PropertyValidationException($"{propertyName} some values are not allowed", propertyName);
            }
        }

    }

    public interface ICustomValidator<in T>
        where T : class, new()
    {
        void ValidateModel(T model);
    }
}
