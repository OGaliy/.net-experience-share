namespace QuartzExample.Application.ViewModels;

public class NoteViewModel
{
    public int Id { get; set; }

    public string Content { get; set; }

    public DateTimeOffset CreatedAt { get; set; }

    public int CreatedBy { get; set; }

    public DateTimeOffset? ModifiedAt { get; set; }

    public int? ModifiedBy { get; set; }

    public int TicketId { get; set; }
}
