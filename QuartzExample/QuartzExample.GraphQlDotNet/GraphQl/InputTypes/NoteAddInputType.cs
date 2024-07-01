using GraphQL.Types;
using QuartzExample.Application.Models;

namespace QuartzExample.GraphQlDotNet.GraphQl.InputTypes;

public class NoteAddInputType : InputObjectGraphType<NoteAddModel>
{
    public NoteAddInputType()
    {
        Name = "noteAddInput";

        Field(x => x.TicketId);
        Field(x => x.Content);
        Field(x => x.CreatedBy);

    }
}
