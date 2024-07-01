using GraphQL.Types;
using MediatR;
using QuartzExample.Application.Queries;
using QuartzExample.Application.ViewModels;

namespace QuartzExample.GraphQlDotNet.GraphQl.Types;

public class TicketType : ObjectGraphType<TicketViewModel>
{
    public TicketType()
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

        Field<ListGraphType<NoteType>>("notes")
            .ResolveAsync(async context =>
            {
                var id = context.Source.Id;
                var mediator = context.RequestServices!.GetRequiredService<IMediator>();
                var notes = await mediator.Send(new NotesGetQuery(id));
                return notes;
            });
    }
}
