namespace QuartzExample.Application.Models;

public class NoteUpdateModel
{
    public int TicketId { get; set; }

    public int NoteId { get; set; }

    public string Content { get; set; }

    public int ByUserId { get; set; }
}
