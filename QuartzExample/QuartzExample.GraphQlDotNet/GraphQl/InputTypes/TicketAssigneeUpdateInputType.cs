using GraphQL.Types;
using QuartzExample.Application.Models;

namespace QuartzExample.GraphQlDotNet.GraphQl.InputTypes;

public class TicketAssigneeUpdateInputType : InputObjectGraphType<TicketAssigneeUpdateModel>
{
    public TicketAssigneeUpdateInputType()
    {
        Name = "ticketAssigneeUpdateInput";

        Field(x => x.TicketId);
        Field(x => x.NewAssignee);
        Field(x => x.ByUserId);
    }
}
