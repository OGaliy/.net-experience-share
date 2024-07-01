using Ardalis.Specification;
using QuartzExample.Domain.Aggregates.TicketAggregate;

namespace QuartzExample.Domain.Specifications;

public class TicketsByIdsSpec : Specification<Ticket>
{
    public TicketsByIdsSpec(List<int> ids)
    {
        Query.Where(x => ids.Contains(x.Id));
    }
}
