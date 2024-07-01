using GraphQL.Types;
using QuartzExample.Application.ViewModels;

namespace QuartzExample.GraphQlDotNet.GraphQl.Types;

public class NoteType : ObjectGraphType<NoteViewModel>
{
    public NoteType()
    {
        Field(x => x.Id);
        Field(x => x.TicketId);
        Field(x => x.Content);
        Field(x => x.CreatedAt);
        Field(x => x.CreatedBy);
        Field(x => x.ModifiedAt, nullable: true);
        Field(x => x.ModifiedBy, nullable: true);
    }
}
