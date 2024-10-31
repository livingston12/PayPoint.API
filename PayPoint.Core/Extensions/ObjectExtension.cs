using System.Linq.Expressions;

namespace PayPoint.Core.Extensions;

public static class ObjectExtension
{
    public static List<Expression<Func<TEntity, object>>> AddIncludesValues<TEntity>(this List<Expression<Func<TEntity, object>>>? includes, Dictionary<object, bool?> includeValues)
        where TEntity : class
    {
        List<Expression<Func<TEntity, object>>> includesExpressions = includeValues.BuildIncludeExpressions<TEntity>();

        if (includes.IsNullOrEmpty())
        {
            return includesExpressions;
        }
        
        includes!.AddRange(includesExpressions);

        return includes;
    }

    private static List<Expression<Func<TEntity, object>>> BuildIncludeExpressions<TEntity>(this Dictionary<object, bool?>? includeValues)
    {
        var expressions = new List<Expression<Func<TEntity, object>>>();

        if (includeValues.IsNullOrEmpty() || includeValues!.All(x => x.Value == false))
        {
            return expressions;
        }

        foreach (var kvp in includeValues!.Where(x => x.Value != false))
        {
            string propertyName = kvp.Key.ToString()!;

            ParameterExpression parameter = Expression.Parameter(typeof(TEntity), "entity");
            MemberExpression property = Expression.Property(parameter, propertyName);
            UnaryExpression? converted = Expression.Convert(property, typeof(object));

            var lambda = Expression.Lambda<Func<TEntity, object>>(converted, parameter);
            expressions.Add(lambda);
        }

        return expressions;
    }
}
