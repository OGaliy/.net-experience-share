using GraphQL;
using GraphQL.Types;
using MediatR;
using QuartzExample.Application.Queries;
using QuartzExample.GraphQlDotNet.GraphQl.Types;

namespace QuartzExample.GraphQlDotNet.GraphQl;

public class TicketQuery : ObjectGraphType
{
    public TicketQuery()
    {
        Field<ListGraphType<TicketType>>("tickets")
            .Arguments(new QueryArgument<NonNullGraphType<ListGraphType<IntGraphType>>> { Name = "ids", Description = "" })
            .ResolveAsync(async context =>
            {
                var ticketIds = context.GetArgument<List<int>>("ids");
                var mediator = context.RequestServices!.GetRequiredService<IMediator>();
                var tickets = await mediator.Send(new TicketsGetQuery(ticketIds));

                return tickets;
            });

        Field<ListGraphType<NoteType>>("notes")
            .Arguments(new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "ticketId", Description = "" })
            .ResolveAsync(async context =>
            {
                var ticketId = context.GetArgument<int>("ticketId");
                var mediator = context.RequestServices!.GetRequiredService<IMediator>();
                var notes = await mediator.Send(new NotesGetQuery(ticketId));

                return notes;
            });
    }
}
