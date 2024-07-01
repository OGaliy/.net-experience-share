using Quartz;
using QuartzExample.Application.Services;
using QuartzExample.Application.ViewModels;
using QuartzExample.Domain.Aggregates.TicketAggregate;
using QuartzExample.Domain.Specifications;
using QuartzExample.Infrastructure.Data.Repositories;

namespace QuartzExample.Infrastructure.Jobs;

public class TicketModifiedJob(
    IRepository<Ticket> repository,
    IStreamTicketModifiedService stream) : IJob
{
    private readonly IRepository<Ticket> _repository = repository;
    private readonly IStreamTicketModifiedService _stream = stream;
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
            var ticketVM = new TicketViewModel
            {
                Id = ticket.Id,
                Title = ticket.Title,
                Description = ticket.Description,
                AssignedId = ticket.AssigneeId,
                DeadLine = ticket.DeadLineDate,
                CreatedAt = ticket.Created.At,
                CreatedBy = ticket.Created.ByUserId,
                ModifiedAt = ticket.Modified?.At,
                ModifiedBy = ticket.Modified?.ByUserId,
                Notes = ticket.Notes.Select(n => new NoteViewModel
                {
                    Id = n.Id,
                    Content = n.Content,
                    CreatedAt = n.Created.At,
                    CreatedBy = n.Created.ByUserId,
                    ModifiedAt = n.Modified?.At,
                    ModifiedBy = n.Modified?.ByUserId
                }).ToList()
            };

            _stream.AddMessage(ticketVM);
            _lastUpdated = DateTimeOffset.UtcNow;
        }
    }
}
