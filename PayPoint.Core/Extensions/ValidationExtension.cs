using System.Collections;

namespace PayPoint.Core.Extensions;

public static class ValidationExtension
{
    public static bool IsNullOrEmpty(this IEnumerable? obj) => obj.IsNullOrEmpty<IEnumerable>();
    public static bool IsNotNullOrEmpty(this IEnumerable? obj) => !obj.IsNullOrEmpty();
    public static bool IsNullOrEmpty<T>(this T? obj) where T : class => obj is null;

    public static bool IsNullOrEmpty<T>(this object? obj)
    {
        if (obj == null)
            return true;

        if (obj is string str)
            return string.IsNullOrEmpty(str);

        if (obj is IEnumerable enumerable)
        {
            return !enumerable.GetEnumerator().MoveNext();
        }

        return false;
    }

    public static bool IsNotNullOrEmpty<T>(this object? obj)
    {
        return !IsNullOrEmpty<T>(obj);
    }
}
