using Ardalis.Specification;
using QuartzExample.Domain.Aggregates.TicketAggregate;

namespace QuartzExample.Domain.Specifications;

public class TicketUpdateAfterDateWithNotesSpec : Specification<Ticket>
{
    public TicketUpdateAfterDateWithNotesSpec(DateTimeOffset date)
    {
        Query.Where(t => t.Created.At > date || t.Modified.At > date)
            .Include(t => t.Notes);
    }
}
