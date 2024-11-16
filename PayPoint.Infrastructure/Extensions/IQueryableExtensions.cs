using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace PayPoint.Infrastructure.Extensions;

public static class IQueryableExtensions
{
    public static IQueryable<T> IncludeAll<T>(this IQueryable<T> query) where T : class
    {
        // Obtener todas las propiedades de la entidad que son de tipo navegación
        var navigationProperties = typeof(T).GetProperties()
            .Where(p => p.PropertyType.IsClass && p.PropertyType != typeof(string));

        foreach (var property in navigationProperties)
        {
            query = query.Include(property.Name);
        }

        return query;
    }
}
