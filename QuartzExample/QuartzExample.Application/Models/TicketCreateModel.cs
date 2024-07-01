namespace QuartzExample.Application.Models;

public class TicketCreateModel
{
    public string Title { get; set; }

    public string Description { get; set; }

    public int Assignee { get; set; }

    public int CreatedBy { get; set; }

    public DateTimeOffset DeadLine { get; set; }

    public List<NoteAddModel> Notes { get; set; }
}
