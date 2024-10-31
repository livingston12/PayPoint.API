namespace PayPoint.Core.Extensions;

public static class ComparisonExtension
{
    /// <summary>
    /// Determines if the value is greater than the expected value.
    /// If the value is null, the result is false.
    /// </summary>
    /// <typeparam name="T">The type of the value and expected value</typeparam>
    /// <param name="value">The value to compare</param>
    /// <param name="expectedValue">The expected value</param>
    /// <returns>True if the value is greater than the expected value, false otherwise</returns>
    public static bool IsGreaterThan<T>(this T? value, T expetedValue) where T : struct, IComparable
    {
        if (value.HasValue)
        {
            return value.Value.CompareTo(expetedValue) > 0;
        }
        return false;
    }

    /// <summary>
    /// Determines if the value is less than the expected value.
    /// If the value is null, the result is false.
    /// </summary>
    /// <typeparam name="T">The type of the value and expected value</typeparam>
    /// <param name="value">The value to compare</param>
    /// <param name="expectedValue">The expected value</param>
    /// <returns>True if the value is less than the expected value, false otherwise</returns>
    public static bool IsLessThan<T>(this T? value, T expectedValue) where T : struct, IComparable
    {
        if (value.HasValue)
        {
            return value.Value.CompareTo(expectedValue) < 0;
        }
        return false;
    }

    /// <summary>
    /// Determines if the value is equal to the expected value.
    /// If the value is null, the result is false.
    /// </summary>
    /// <typeparam name="T">The type of the value and expected value</typeparam>
    /// <param name="value">The value to compare</param>
    /// <param name="expectedValue">The expected value</param>
    /// <returns>True if the value is equal to the expected value, false otherwise</returns>
    public static bool IsEqualTo<T>(this T? value, T expectedValue) where T : struct, IComparable
    {
        if (value.HasValue)
        {
            return value.Value.CompareTo(expectedValue) == 0;
        }
        return false;
    }

    /// <summary>
    /// Determines if the value is greater than or equal to the expected value.
    /// If the value is null, the result is false.
    /// </summary>
    /// <typeparam name="T">The type of the value and expected value</typeparam>
    /// <param name="value">The value to compare</param>
    /// <param name="expectedValue">The expected value</param>
    /// <returns>True if the value is greater than or equal to the expected value, false otherwise</returns>
    public static bool IsGreaterThanOrEqualTo<T>(this T? value, T expectedValue) where T : struct, IComparable
    {
        if (value.HasValue)
        {
            return value.Value.CompareTo(expectedValue) >= 0;
        }
        return false;
    }

    /// <summary>
    /// Determines if the value is less than or equal to the expected value.
    /// If the value is null, the result is false.
    /// </summary>
    /// <typeparam name="T">The type of the value and expected value</typeparam>
    /// <param name="value">The value to compare</param>
    /// <param name="expectedValue">The expected value</param>
    /// <returns>True if the value is less than or equal to the expected value, false otherwise</returns>
    public static bool IsLessThanOrEqualTo<T>(this T? value, T expectedValue) where T : struct, IComparable
    {
        if (value.HasValue)
        {
            return value.Value.CompareTo(expectedValue) <= 0;
        }
        return false;
    }
}
