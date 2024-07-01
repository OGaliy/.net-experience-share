using GraphQL.Types;
using QuartzExample.Application.Models;

namespace QuartzExample.GraphQlDotNet.GraphQl.InputTypes;

public class TicketCreateInputType : InputObjectGraphType<TicketCreateModel>
{
    public TicketCreateInputType()
    {
        Name = "ticketCreateInput";

        Field(x => x.Title);
        Field(x => x.Description);
        Field(x => x.CreatedBy);
        Field(x => x.Assignee);
        Field(x => x.DeadLine, nullable: true);
        Field<ListGraphType<NoteAddInputType>>("notes");
    }
}
