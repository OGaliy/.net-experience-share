using MediatR;
using QuartzExample.Application.Commands;
using QuartzExample.Domain.Aggregates.TicketAggregate;
using QuartzExample.Domain.Exceptions;
using QuartzExample.Domain.Specifications;
using QuartzExample.Infrastructure.Data.Repositories;

namespace QuartzExample.Infrastructure.CommandHandlers;

public class NoteAddCommandHandler(
    IRepository<Ticket> repository) : IRequestHandler<NoteAddCommand, bool>
{
    private readonly IRepository<Ticket> _repository = repository;

    public async Task<bool> Handle(NoteAddCommand request, CancellationToken cancellationToken)
    {
        var ticket = (await _repository.GetBySpecAsync(new TicketByIdWithNotesSpec(request.TicketId))).FirstOrDefault();

        if (ticket == null)
        {
            throw new TicketNotFoundException(request.TicketId);
        }

        var note = new Note(request.Content, request.CreatedBy, DateTimeOffset.UtcNow);

        ticket.AddNote(note);
        await _repository.SaveChangesAsync();

        return true;
    }
}
