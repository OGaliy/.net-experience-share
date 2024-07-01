using MediatR;
using QuartzExample.Application.Commands;
using QuartzExample.Domain.Aggregates.TicketAggregate;
using QuartzExample.Domain.Exceptions;
using QuartzExample.Domain.Specifications;
using QuartzExample.Infrastructure.Data.Repositories;

namespace QuartzExample.Infrastructure.CommandHandlers;

public class NoteRemoveCommandHandler(
    IRepository<Ticket> repository) : IRequestHandler<NoteRemoveCommand, bool>
{
    private readonly IRepository<Ticket> _repository = repository;

    public async Task<bool> Handle(NoteRemoveCommand request, CancellationToken cancellationToken)
    {
        var ticket = (await _repository.GetBySpecAsync(new TicketByIdWithNotesSpec(request.TicketId))).FirstOrDefault();

        if (ticket == null)
        {
            throw new TicketNotFoundException(request.TicketId);
        }

        ticket.RemoveNote(request.NoteId, request.ByUserId);
        await _repository.SaveChangesAsync();

        return true;
    }
}
