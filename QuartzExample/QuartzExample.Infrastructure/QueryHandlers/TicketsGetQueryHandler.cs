using AutoMapper;
using MediatR;
using QuartzExample.Application.Queries;
using QuartzExample.Application.ViewModels;
using QuartzExample.Domain.Aggregates.TicketAggregate;
using QuartzExample.Domain.Specifications;
using QuartzExample.Infrastructure.Data.Repositories;

namespace QuartzExample.Infrastructure.QueryHandlers;

public class TicketsGetQueryHandler(
    IRepository<Ticket> repository,
    IMapper mapper) : IRequestHandler<TicketsGetQuery, IEnumerable<TicketViewModel>>
{
    private readonly IRepository<Ticket> _repository = repository;
    private readonly IMapper _mapper = mapper;

    public async Task<IEnumerable<TicketViewModel>> Handle(TicketsGetQuery request, CancellationToken cancellationToken)
    {
        var tickets = await _repository.GetBySpecAsync(new TicketsByIdsSpec(request.TicketIds));

        return _mapper.Map<IEnumerable<TicketViewModel>>(tickets);
    }
}
