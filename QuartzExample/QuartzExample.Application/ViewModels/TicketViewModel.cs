namespace QuartzExample.Application.ViewModels;

public class TicketViewModel
{
    public int Id { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }

    public int AssignedId { get; set; }

    public DateTimeOffset CreatedAt { get; set; }

    public int CreatedBy { get; set; }

    public DateTimeOffset? ModifiedAt { get; set; }

    public int? ModifiedBy { get; set; }

    public DateTimeOffset? DeadLine { get; set; }

    public List<NoteViewModel> Notes { get; set; }
}
