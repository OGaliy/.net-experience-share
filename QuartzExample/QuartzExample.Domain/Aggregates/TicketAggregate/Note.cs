using Ardalis.GuardClauses;
using QuartzExample.Domain.Base;
using QuartzExample.Domain.ValueObject;

namespace QuartzExample.Domain.Aggregates.TicketAggregate;

public class Note : Entity
{
    public Note()
    {
        // Ef
    }

    public Note(
        string content,
        int createdBy,
        DateTimeOffset createdAt) : this()
    {
        Guard.Against.NullOrWhiteSpace(content, nameof(content));
        Guard.Against.Negative(createdBy, nameof(createdBy));
        Guard.Against.Default(createdAt, nameof(createdAt));

        Content = content;
        Created = new(createdAt, createdBy);
    }

    public string Content { get; private set; }

    public AuditAction Created { get; private set; }

    public AuditAction? Modified { get; private set; }

    public int TicketId { get; private set; }

    public void UpdateContent(string content)
    {
        Guard.Against.NullOrWhiteSpace(content, nameof(content));

        Content = content;
        Modified = new(DateTimeOffset.UtcNow, Created.ByUserId);
    }
}
