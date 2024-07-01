using AutoMapper;
using Quartz;
using QuartzExample.Application.Services;
using QuartzExample.Application.ViewModels;
using QuartzExample.Domain.Aggregates.TicketAggregate;
using QuartzExample.Domain.Specifications;
using QuartzExample.Infrastructure.Data.Repositories;

namespace QuartzExample.Infrastructure.Jobs;

public class TicketModifiedJob(
    IRepository<Ticket> repository,
    IStreamTicketModifiedService stream,
    IMapper mapper) : IJob
{
    private readonly IRepository<Ticket> _repository = repository;
    private readonly IStreamTicketModifiedService _stream = stream;
    private readonly IMapper _mapper = mapper;
    private static DateTimeOffset _lastUpdated = DateTimeOffset.MinValue;

    public async Task Execute(IJobExecutionContext context)
    {
        if (_lastUpdated == DateTimeOffset.MinValue)
        {
            _lastUpdated = DateTimeOffset.UtcNow;
        }

        var tickets = await _repository.GetBySpecAsync(new TicketUpdateAfterDateWithNotesSpec(_lastUpdated));

        if (!tickets.Any())
        {
            return;
        }

        foreach (var ticket in tickets)
        {
            var ticketVM = _mapper.Map<TicketViewModel>(ticket);

            _stream.AddMessage(ticketVM);
            _lastUpdated = DateTimeOffset.UtcNow;
        }
    }
}
