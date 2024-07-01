using Ardalis.GuardClauses;

namespace QuartzExample.Domain.Guards;

public static class DateTimeOffsetGuardExtensions
{
    public static void NullOrAgainstPast(this IGuardClause guardClause, DateTimeOffset? input, string parameterName)
    {
        if (input == null)
        {
            return;
        }
        if (input < DateTimeOffset.Now)
        {
            throw new ArgumentOutOfRangeException(parameterName, $"{parameterName} should be in the future.");
        }
    }
}
