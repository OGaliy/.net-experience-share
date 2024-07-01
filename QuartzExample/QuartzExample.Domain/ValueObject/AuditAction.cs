using Ardalis.GuardClauses;

namespace QuartzExample.Domain.ValueObject;

public class AuditAction
{
    public AuditAction()
    {
        // EF Core
    }

    public AuditAction(
        DateTimeOffset at,
        int byUserId)
    {
        Guard.Against.Default(at, nameof(at));
        Guard.Against.Negative(byUserId, nameof(byUserId));

        At = at;
        ByUserId = byUserId;
    }

    public DateTimeOffset At { get; }

    public int ByUserId { get; }
}
