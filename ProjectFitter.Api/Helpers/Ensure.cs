using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace ProjectFitter.Api.Helpers
{
    public static class Ensure
    {
        public static void NotNullOrEmpty(
            [NotNull] string? value,
            [CallerArgumentExpression("value")] string? paramName = null)
        {
            if (string.IsNullOrEmpty(value)) throw new ArgumentNullException(paramName);
        }

        public static void NotNull(
            [NotNull] object? value,
            [CallerArgumentExpression("value")] string? paramName = null)
        {
            if (value is null) throw new ArgumentNullException(paramName);
        }

        public static void GreaterThanZero(
            decimal value,
            [CallerArgumentExpression("value")] string? paramName = null)
        {
            if (value <= 0) throw new ArgumentOutOfRangeException(paramName);
        }

        public static void StartDatePrecedesEndDate(
            DateTime start,
            DateTime end,
            [CallerArgumentExpression("end")] string? paramName = null)
        {
            if (start >= end) throw new ArgumentOutOfRangeException(paramName);
        }
    }
}
