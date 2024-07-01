using MediatR;
using QuartzExample.Application.Commands;
using QuartzExample.Domain.Aggregates.TicketAggregate;
using QuartzExample.Domain.Exceptions;
using QuartzExample.Domain.Specifications;
using QuartzExample.Infrastructure.Data.Repositories;

namespace QuartzExample.Infrastructure.CommandHandlers;

public class TicketDescriptionUpdateCommandHandler(IRepository<Ticket> repository) : IRequestHandler<TicketDescriptionUpdateCommand, bool>
{
    private readonly IRepository<Ticket> _repository = repository;

    public async Task<bool> Handle(TicketDescriptionUpdateCommand request, CancellationToken cancellationToken)
    {
        var ticket = await _repository.GetAsync(request.TicketId);

        if (ticket == null)
        {
            throw new TicketNotFoundException(request.TicketId);
        }

        ticket.UpdateDescription(request.Description, request.ByUserId);
        await _repository.SaveChangesAsync();

        return true;
    }
}
