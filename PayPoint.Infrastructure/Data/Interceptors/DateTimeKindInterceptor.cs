using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;
using PayPoint.Core.Extensions;

namespace PayPoint.Infrastructure.Data.Interceptors;

public class DateTimeKindInterceptor : SaveChangesInterceptor
{
    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        DbContext? context = eventData.Context;
        
        if (context.IsNullOrEmpty())
        {
            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        foreach (EntityEntry? entry in context!.ChangeTracker.Entries())
        {
            if (entry.State == EntityState.Added || entry.State == EntityState.Modified)
            {
                foreach (PropertyEntry? property in entry.Properties.Where(property => property.Metadata.ClrType == typeof(DateTime) && property.CurrentValue is DateTime))
                {
                    if (property.CurrentValue is DateTime dateTime)
                    {
                        // Explicitly convert to UTC if necessary.
                        property.CurrentValue = DateTime.SpecifyKind(dateTime, DateTimeKind.Utc);
                    }
                }
            }
        }

        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }
}
