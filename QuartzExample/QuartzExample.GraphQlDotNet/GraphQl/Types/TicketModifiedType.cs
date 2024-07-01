using GraphQL.Types;
using QuartzExample.Application.ViewModels;

namespace QuartzExample.GraphQlDotNet.GraphQl.Types;

public class TicketModifiedType : ObjectGraphType<TicketViewModel>
{
    public TicketModifiedType()
    {
        Field(x => x.Id);
        Field(x => x.Title);
        Field(x => x.Description);
        Field(x => x.CreatedAt);
        Field(x => x.CreatedBy);
        Field(x => x.AssignedId);
        Field(x => x.DeadLine, nullable: true);
        Field(x => x.ModifiedBy, nullable: true);
        Field(x => x.ModifiedAt, nullable: true);

        Field<ListGraphType<NoteType>>("notes");
    }
}
