using GraphQL.Types;
using QuartzExample.Application.Models;

namespace QuartzExample.GraphQlDotNet.GraphQl.InputTypes;

public class NoteRemoveInputType : InputObjectGraphType<NoteRemoveModel>
{
    public NoteRemoveInputType()
    {
        Name = "noteRemoveInput";

        Field(x => x.TicketId);
        Field(x => x.NoteId);
        Field(x => x.ByUserId);

    }
}
