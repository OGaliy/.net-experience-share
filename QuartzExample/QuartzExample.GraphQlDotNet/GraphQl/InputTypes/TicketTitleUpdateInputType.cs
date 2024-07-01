using GraphQL.Types;
using QuartzExample.Application.Models;

namespace QuartzExample.GraphQlDotNet.GraphQl.InputTypes;

public class TicketTitleUpdateInputType : InputObjectGraphType<TicketTitleUpdateModel>
{
    public TicketTitleUpdateInputType()
    {
        Name = "ticketTitleUpdateInput";

        Field(x => x.TicketId);
        Field(x => x.Title);
        Field(x => x.ByUserId);
    }
}
