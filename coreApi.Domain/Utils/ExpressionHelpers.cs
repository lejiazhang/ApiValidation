using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using static System.String;

namespace coreApi.Domain.Utils
{
    public static class ExpressionHelpers
    {

        public static IEnumerable<PropertyInfo> GetProperties(Type type)
        {
            if (type == null)
                return new List<PropertyInfo>();

            var plist = type.GetProperties().Where(dest => dest.CanWrite &&
                    (dest.PropertyType.IsValueType || dest.PropertyType == typeof(string))
                    && dest.DeclaringType == dest.ReflectedType
                ).ToList();

            plist.AddRange(GetProperties(type.BaseType));

            return plist;
        }

        public static PropertyInfo GetPropertyInfo(Type type, string propertyName)
        {
            var result = type.GetProperties()
                 .FirstOrDefault(dest => dest.CanWrite &&
                     (dest.PropertyType.IsValueType || dest.PropertyType == typeof(string))
                     && dest.DeclaringType == dest.ReflectedType
                     && Compare(dest.Name, propertyName, StringComparison.OrdinalIgnoreCase) == 0);

            if (result != null)
                return result;

            return GetPropertyInfo(type.BaseType, propertyName);
        }


        public static Expression<Func<T, object>> GetPropertySelector<T>(string propertyName)
        {
            var arg = Expression.Parameter(typeof(T), "x");
            var property = Expression.Property(arg, GetPropertyInfo(typeof(T), propertyName));
            //return the property as object
            var conv = Expression.Convert(property, typeof(object));
            var exp = Expression.Lambda<Func<T, object>>(conv, new ParameterExpression[] { arg });
            return exp;
        }


        public static string GetPropertyName<TModel, TProperty>(Expression<Func<TModel, TProperty>> property)
        {
            MemberExpression memberExpression = null;
            if (property.Body.NodeType == ExpressionType.Convert)
            {
                memberExpression =
                    ((UnaryExpression)property.Body).Operand as MemberExpression;
            }
            else if (property.Body.NodeType == ExpressionType.MemberAccess)
            {
                memberExpression = property.Body as MemberExpression;
            }

            if (memberExpression == null)
                throw new ArgumentException("method");

            return memberExpression.Member.GetCustomAttribute<DisplayNameAttribute>()?.DisplayName ?? memberExpression.Member.Name;
        }

        public static PropertyInfo GetPropertyInfo<TModel, TProperty>(Expression<Func<TModel, TProperty>> property)
        {
            MemberExpression memberExpression = null;
            if (property.Body.NodeType == ExpressionType.Convert)
            {
                memberExpression =
                    ((UnaryExpression)property.Body).Operand as MemberExpression;
            }
            else if (property.Body.NodeType == ExpressionType.MemberAccess)
            {
                memberExpression = property.Body as MemberExpression;
            }

            if (memberExpression == null)
                throw new ArgumentException("method");

            return memberExpression.Member as PropertyInfo;
        }
    }
}