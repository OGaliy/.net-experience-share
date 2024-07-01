using MediatR;
using QuartzExample.Application.Commands;
using QuartzExample.Domain.Aggregates.TicketAggregate;
using QuartzExample.Domain.Exceptions;
using QuartzExample.Domain.Specifications;
using QuartzExample.Infrastructure.Data.Repositories;

namespace QuartzExample.Infrastructure.CommandHandlers;

public class TicketAssigneeUpdateCommandHandler(IRepository<Ticket> repository) : IRequestHandler<TicketAssigneeUpdateCommand, bool>
{
    private readonly IRepository<Ticket> _repository = repository;

    public async Task<bool> Handle(TicketAssigneeUpdateCommand request, CancellationToken cancellationToken)
    {
        var ticket = await _repository.GetAsync(request.TicketId);

        if (ticket == null)
        {
            throw new TicketNotFoundException(request.TicketId);
        }

        ticket.ReAssignee(request.AssigneeId, request.ByUserId);
        await _repository.SaveChangesAsync();

        return true;
    }
}
