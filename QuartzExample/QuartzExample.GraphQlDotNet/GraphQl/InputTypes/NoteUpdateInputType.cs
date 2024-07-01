using GraphQL.Types;
using QuartzExample.Application.Models;

namespace QuartzExample.GraphQlDotNet.GraphQl.InputTypes;

public class NoteUpdateInputType : InputObjectGraphType<NoteUpdateModel>
{
    public NoteUpdateInputType()
    {
        Name = "noteUpdateInput";

        Field(x => x.TicketId);
        Field(x => x.NoteId);
        Field(x => x.Content);
        Field(x => x.ByUserId);
    }
}
