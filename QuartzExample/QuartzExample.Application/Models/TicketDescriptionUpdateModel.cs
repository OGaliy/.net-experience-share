namespace QuartzExample.Application.Models;

public class TicketDescriptionUpdateModel
{
    public int TicketId { get; set; }

    public string Description { get; set; }

    public int ByUserId { get; set; }
}
