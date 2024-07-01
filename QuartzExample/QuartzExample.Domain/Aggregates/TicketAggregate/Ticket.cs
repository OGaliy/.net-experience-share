using Ardalis.GuardClauses;
using QuartzExample.Domain.Base;
using QuartzExample.Domain.ValueObject;

namespace QuartzExample.Domain.Aggregates.TicketAggregate;

public class Ticket : Entity, IAggregateRoot
{
    private readonly List<Note> _notes;

    public Ticket()
    {
        _notes = [];
    }

    public Ticket(
        string title,
        string description,
        int assigneeId,
        DateTimeOffset createdAt,
        int createdBy,
        DateTimeOffset? deadLineDate = null,
        List<Note>? notes = null) : this()
    {
        Guard.Against.NullOrWhiteSpace(title, nameof(title));
        Guard.Against.NullOrWhiteSpace(description, nameof(description));
        Guard.Against.NegativeOrZero(assigneeId, nameof(assigneeId));
        Guard.Against.Negative(createdBy, nameof(createdBy));
        Guard.Against.Default(createdAt, nameof(createdAt));

        Title = title;
        Description = description;
        AssigneeId = assigneeId;
        DeadLineDate = deadLineDate;

        Created = new(createdAt, createdBy);

        _notes = notes ?? _notes;
    }

    public string Title { get; private set; }

    public string Description { get; private set; }

    public int AssigneeId { get; private set; }

    public DateTimeOffset? DeadLineDate { get; private set; }

    public AuditAction Created { get; private set; }

    public AuditAction Modified { get; private set; }

    public IReadOnlyCollection<Note> Notes => _notes;

    public void AddNote(Note note)
    {
        _notes.Add(note);
        Modified = new(DateTimeOffset.UtcNow, note.Created.ByUserId);
    }
    
    public void RemoveNote(int noteId, int byUserId)
    {
        var note = _notes.FirstOrDefault(x => x.Id == noteId);
        if (note != null)
        {
            _notes.Remove(note);
            Modified = new(DateTimeOffset.UtcNow, byUserId);
        }
    }

    public void UpdateTitle(string title, int byUserId)
    {
        Title = title;
        Modified = new(DateTimeOffset.UtcNow, byUserId);
    }

    public void UpdateDescription(string description, int byUserId)
    {
        Description = description;
        Modified = new(DateTimeOffset.UtcNow, byUserId);
    }

    public void ReAssignee(int assigneeId, int byUserId)
    {
        AssigneeId = assigneeId;
        Modified = new(DateTimeOffset.UtcNow, byUserId);
    }
}
