namespace PayPoint.Core.Extensions;

public static class SafeExtension
{
    public static List<T> ToSafeList<T>(this IEnumerable<T>? input) where T : struct
    {
        if (input.IsNullOrEmpty())
        {
            return new List<T>();
        }

        return input!.ToList();
    }

    public static List<T> ToSafeList<T>(this IEnumerable<T?> input) where T : struct
    {
        if (input.IsNullOrEmpty())
        {
            return new List<T>();
        }

        return input.Where(item => item.HasValue)
            .Select(item => item!.Value)
            .ToList();
    }

    public static List<T> ToSafeList<T>(this ICollection<T>? input)
    {
        if (input.IsNullOrEmpty())
        {
            return new List<T>();
        }

        return input!.ToList();
    }

    public static List<T> ToSafeList<T>(this ICollection<T?> input) where T : struct
    {
        if (input.IsNullOrEmpty())
        {
            return new List<T>();
        }

        return input.Where(item => item.HasValue)
            .Select(item => item!.Value)
            .ToList();
    }
}
