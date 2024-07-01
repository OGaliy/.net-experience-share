using MediatR;
using QuartzExample.Application.Commands;
using QuartzExample.Domain.Aggregates.TicketAggregate;
using QuartzExample.Domain.Exceptions;
using QuartzExample.Domain.Specifications;
using QuartzExample.Infrastructure.Data.Repositories;

namespace QuartzExample.Infrastructure.CommandHandlers;

public class NoteUpdateCommandHandler(
    IRepository<Ticket> repository) : IRequestHandler<NoteUpdateCommand, bool>
{
    private readonly IRepository<Ticket> _repository = repository;

    public async Task<bool> Handle(NoteUpdateCommand request, CancellationToken cancellationToken)
    {
        var ticket = (await _repository.GetBySpecAsync(new TicketByIdWithNotesSpec(request.TicketId))).FirstOrDefault();

        if (ticket == null)
        {
            throw new TicketNotFoundException(request.TicketId);
        }

        var note = ticket.Notes.FirstOrDefault(n => n.Id == request.NoteId);

        if (note == null)
        {
            throw new NoteNotFoundException(request.TicketId, request.NoteId);
        }

        note.UpdateContent(request.Content);
        await _repository.SaveChangesAsync();

        return true;
    }
}
