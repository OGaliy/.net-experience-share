using Ardalis.Specification;
using Microsoft.EntityFrameworkCore;
using Ardalis.Specification.EntityFrameworkCore;
using QuartzExample.Domain.Base;

namespace QuartzExample.Infrastructure.Data.Repositories;

public abstract class RepositoryBase<T>(TicketDbContext context) : IRepository<T> where T : Entity, IAggregateRoot
{
    private readonly TicketDbContext _context = context;

    public T Add(T entity)
    {
        return _context.Set<T>().Add(entity).Entity;
    }

    public void Delete(T entity)
    {
        _context.Entry(entity).State = EntityState.Deleted;
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _context.Set<T>().ToListAsync();
    }

    public async Task<T?> GetAsync(int id)
    {
        var entity = await _context.Set<T>().FirstOrDefaultAsync(x => x.Id == id);

        entity ??= _context.Set<T>().Local.FirstOrDefault(x => x.Id == id);

        return entity;
    }

    public async Task<IEnumerable<T>> GetBySpecAsync(Specification<T> spec)
    {
        return await _context.Set<T>().WithSpecification(spec).ToListAsync();
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }

    public void Update(T entity)
    {
        _context.Entry(entity).State = EntityState.Modified;
    }
}
