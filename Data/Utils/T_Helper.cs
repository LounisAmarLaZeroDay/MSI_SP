using Data.Model;
using LaZeroDayCore.Controller;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Data.Utils
{
    public static partial class T_Helper
    {
        private static Entities _db = Entities.GetInstance();
        public static bool IsNull()
        {
            return (Entities.GetInstance() == null);
        }
        public static IOrderedQueryable<TSource> OrderBy<TSource>(this IQueryable<TSource> query, string propertyName)
        {
            var entityType = typeof(TSource);
            var propertyInfo = entityType.GetProperty(propertyName);
            ParameterExpression arg = Expression.Parameter(entityType, "x");
            MemberExpression property = Expression.Property(arg, propertyName);
            var selector = Expression.Lambda(property, new ParameterExpression[] { arg });
            var enumarableType = typeof(System.Linq.Queryable);
            var method = enumarableType.GetMethods().Where(m => m.Name == "OrderBy" && m.IsGenericMethodDefinition).Where(m =>
            {
                var parameters = m.GetParameters().ToList();
                return parameters.Count == 2;
            }).Single();
            MethodInfo genericMethod = method.MakeGenericMethod(entityType, propertyInfo.PropertyType);
            var newQuery = (IOrderedQueryable<TSource>)genericMethod.Invoke(genericMethod, new object[] { query, selector });
            return newQuery;
        }
        public static IOrderedQueryable<TSource> OrderByDescending<TSource>(this IQueryable<TSource> query, string propertyName)
        {
            var entityType = typeof(TSource);
            var propertyInfo = entityType.GetProperty(propertyName);
            ParameterExpression arg = Expression.Parameter(entityType, "x");
            MemberExpression property = Expression.Property(arg, propertyName);
            var selector = Expression.Lambda(property, new ParameterExpression[] { arg });
            var enumarableType = typeof(System.Linq.Queryable);
            var method = enumarableType.GetMethods().Where(m => m.Name == "OrderByDescending" && m.IsGenericMethodDefinition).Where(m =>
            {
                var parameters = m.GetParameters().ToList();
                return parameters.Count == 2;
            }).Single();
            MethodInfo genericMethod = method.MakeGenericMethod(entityType, propertyInfo.PropertyType);
            var newQuery = (IOrderedQueryable<TSource>)genericMethod.Invoke(genericMethod, new object[] { query, selector });
            return newQuery;
        }
    }
}
