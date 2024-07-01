using Ardalis.Specification;
using QuartzExample.Domain.Aggregates.TicketAggregate;

namespace QuartzExample.Domain.Specifications;

public class TicketByIdWithNotesSpec : Specification<Ticket>
{
    public TicketByIdWithNotesSpec(int id)
    {
        Query.Where(x => x.Id == id).Include(x => x.Notes);
    }
}
