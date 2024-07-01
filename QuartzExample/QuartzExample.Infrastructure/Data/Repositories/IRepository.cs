using Ardalis.Specification;
using QuartzExample.Domain.Base;

namespace QuartzExample.Infrastructure.Data.Repositories;

public interface IRepository<T> where T : IAggregateRoot
{
    T Add(T entity);

    void Update(T entity);

    void Delete(T entity);

    Task<T?> GetAsync(int id);

    Task<IEnumerable<T>> GetAllAsync();

    Task<IEnumerable<T>> GetBySpecAsync(Specification<T> spec);

    Task SaveChangesAsync();
}
