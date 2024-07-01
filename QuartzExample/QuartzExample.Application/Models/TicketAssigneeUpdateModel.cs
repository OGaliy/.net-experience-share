namespace QuartzExample.Application.Models;

public class TicketAssigneeUpdateModel
{
    public int TicketId { get; set; }

    public int NewAssignee { get; set; }

    public int ByUserId { get; set; }
}
