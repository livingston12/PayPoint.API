using System.Collections;

namespace PayPoint.Core.Extensions;

public static class ValidationExtension
{
    public static bool IsNullOrEmpty(this IEnumerable? obj) => obj.IsNullOrEmpty<IEnumerable>();
    public static bool IsNotNullOrEmpty(this IEnumerable? obj) => !obj.IsNullOrEmpty();
    public static bool IsNullOrEmpty<T>(this T? obj) where T : class => obj is null;
    public static bool IsNotNullOrEmpty<T>(this T? obj) where T : class => obj is not null;

    /// <summary>
    /// Determines whether the given object is null or empty.
    /// </summary>
    /// <typeparam name="T">The type of the object.</typeparam>
    /// <param name="obj">The object to test.</param>
    /// <returns>true if obj is null, an empty string, or an empty collection; otherwise, false.</returns>
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

    /// <summary>
    ///     Checks if the object is not null and not empty.
    /// </summary>
    /// <typeparam name="T">The type of the object.</typeparam>
    /// <param name="obj">The object to check.</param>
    /// <returns><c>true</c> if the object is not null and not empty, <c>false</c> otherwise.</returns>
    public static bool IsNotNullOrEmpty<T>(this object? obj)
    {
        return !IsNullOrEmpty<T>(obj);
    }
}
