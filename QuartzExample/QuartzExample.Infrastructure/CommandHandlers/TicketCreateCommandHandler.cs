using Ardalis.GuardClauses;
using MediatR;
using QuartzExample.Application.Commands;
using QuartzExample.Domain.Aggregates.TicketAggregate;
using QuartzExample.Domain.Guards;
using QuartzExample.Infrastructure.Data.Repositories;

namespace QuartzExample.Infrastructure.CommandHandlers;

public class TicketCreateCommandHandler(
    IRepository<Ticket> repository) : IRequestHandler<TicketCreateCommand, int>
{
    private readonly IRepository<Ticket> _repository = repository;

    public async Task<int> Handle(TicketCreateCommand request, CancellationToken cancellationToken)
    {
        Guard.Against.NullOrWhiteSpace(request.Title, nameof(request.Title));
        Guard.Against.NullOrAgainstPast(request.DeadLine, nameof(request.DeadLine));
        Guard.Against.NegativeOrZero(request.AssignedTo, nameof(request.AssignedTo));
        Guard.Against.Negative(request.CreatedBy, nameof(request.CreatedBy));
        var createDate = DateTimeOffset.Now;

        var notes = request.NoteCreateCommands.Select(note => new Note(note.Content, note.CreatedBy, createDate)).ToList();
        var ticket = new Ticket(request.Title, request.Description, request.AssignedTo, createDate, request.CreatedBy, request.DeadLine, notes);

        _repository.Add(ticket);
        await _repository.SaveChangesAsync();

        return ticket.Id;
    }
}
