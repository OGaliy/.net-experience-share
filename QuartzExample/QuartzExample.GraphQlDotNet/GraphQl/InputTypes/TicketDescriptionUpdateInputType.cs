using GraphQL.Types;
using QuartzExample.Application.Models;

namespace QuartzExample.GraphQlDotNet.GraphQl.InputTypes;

public class TicketDescriptionUpdateInputType : InputObjectGraphType<TicketDescriptionUpdateModel>
{
    public TicketDescriptionUpdateInputType()
    {
        Name = "ticketDescriptionUpdateInput";

        Field(x => x.TicketId);
        Field(x => x.Description);
        Field(x => x.ByUserId);
    }
}
