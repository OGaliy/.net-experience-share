using QuartzExample.Domain.Base;

namespace QuartzExample.Infrastructure.Data.Repositories;

public class EfRepository<T> : RepositoryBase<T> where T : Entity, IAggregateRoot
{
    public EfRepository(TicketDbContext context)
        : base(context)
    {
    }
}
