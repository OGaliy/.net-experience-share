using MediatR;
using QuartzExample.Application.Commands;
using QuartzExample.Domain.Aggregates.TicketAggregate;
using QuartzExample.Domain.Exceptions;
using QuartzExample.Infrastructure.Data.Repositories;

namespace QuartzExample.Infrastructure.CommandHandlers;

public class TicketTitleUpdateCommandHandler(IRepository<Ticket> repository) : IRequestHandler<TicketTitleUpdateCommand, bool>
{
    private readonly IRepository<Ticket> _repository = repository;

    public async Task<bool> Handle(TicketTitleUpdateCommand request, CancellationToken cancellationToken)
    {
        var ticket = await _repository.GetAsync(request.TicketId);

        if (ticket == null)
        {
            throw new TicketNotFoundException(request.TicketId);
        }

        ticket.UpdateTitle(request.Title, request.ByUserId);
        await _repository.SaveChangesAsync();

        return true;
    }
}
