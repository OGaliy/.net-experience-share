using GraphQL;
using GraphQL.Types;
using MediatR;
using QuartzExample.Application.Commands;
using QuartzExample.Application.Models;
using QuartzExample.GraphQlDotNet.GraphQl.InputTypes;

namespace QuartzExample.GraphQlDotNet.GraphQl;

public class TicketMutation : ObjectGraphType
{
    private readonly string _input = "input";

    public TicketMutation()
    {
        Field<IntGraphType>("ticketCreate")
            .Argument<NonNullGraphType<TicketCreateInputType>>(_input)
            .ResolveAsync(async context =>
            {
                var ticketCreateModel = context.GetArgument<TicketCreateModel>(_input);
                var notesCommands = ticketCreateModel.Notes.Select(note => new NoteAddCommand(0, note.Content, note.CreatedBy)).ToList();
                var command = new TicketCreateCommand(ticketCreateModel.Title, ticketCreateModel.Description, ticketCreateModel.CreatedBy, ticketCreateModel.Assignee, notesCommands, ticketCreateModel.DeadLine);
                var mediator = context.RequestServices!.GetRequiredService<IMediator>();
                var ticketId = await mediator.Send(command);

                return ticketId;
            });

        Field<BooleanGraphType>("ticketTitleUpdate")
            .Argument<NonNullGraphType<TicketTitleUpdateInputType>>(_input)
            .ResolveAsync(async context =>
            {
                var ticketTitleUpdateModel = context.GetArgument<TicketTitleUpdateModel>(_input);
                var command = new TicketTitleUpdateCommand(ticketTitleUpdateModel.TicketId, ticketTitleUpdateModel.Title, ticketTitleUpdateModel.ByUserId);
                var mediator = context.RequestServices!.GetRequiredService<IMediator>();
                var ticketId = await mediator.Send(command);

                return ticketId;
            });

        Field<BooleanGraphType>("ticketDescriptionUpdate")
            .Argument<NonNullGraphType<TicketDescriptionUpdateInputType>>(_input)
            .ResolveAsync(async context =>
            {
                var ticketDescriptionUpdateModel = context.GetArgument<TicketDescriptionUpdateModel>(_input);
                var command = new TicketDescriptionUpdateCommand(ticketDescriptionUpdateModel.TicketId, ticketDescriptionUpdateModel.Description, ticketDescriptionUpdateModel.ByUserId);
                var mediator = context.RequestServices!.GetRequiredService<IMediator>();
                var ticketId = await mediator.Send(command);

                return ticketId;
            });

        Field<BooleanGraphType>("noteAdd")
            .Argument<NonNullGraphType<NoteAddInputType>>(_input)
            .ResolveAsync(async context =>
            {
                var noteAddModel = context.GetArgument<NoteAddModel>(_input);
                var command = new NoteAddCommand(noteAddModel.TicketId, noteAddModel.Content, noteAddModel.CreatedBy);
                var mediator = context.RequestServices!.GetRequiredService<IMediator>();
                var noteId = await mediator.Send(command);

                return noteId;
            });

        Field<BooleanGraphType>("noteRemove")
            .Argument<NonNullGraphType<NoteRemoveInputType>>(_input)
            .ResolveAsync(async context =>
            {
                var noteRemoveModel = context.GetArgument<NoteRemoveModel>(_input);
                var command = new NoteRemoveCommand(noteRemoveModel.TicketId, noteRemoveModel.NoteId, noteRemoveModel.ByUserId);
                var mediator = context.RequestServices!.GetRequiredService<IMediator>();
                var noteId = await mediator.Send(command);

                return noteId;
            });

        Field<BooleanGraphType>("noteUpdate")
            .Argument<NonNullGraphType<NoteUpdateInputType>>(_input)
            .ResolveAsync(async context =>
            {
                var noteUpdateModel = context.GetArgument<NoteUpdateModel>(_input);
                var command = new NoteUpdateCommand(noteUpdateModel.TicketId, noteUpdateModel.NoteId, noteUpdateModel.Content, noteUpdateModel.ByUserId);
                var mediator = context.RequestServices!.GetRequiredService<IMediator>();
                var noteId = await mediator.Send(command);

                return noteId;
            });
    }
}
