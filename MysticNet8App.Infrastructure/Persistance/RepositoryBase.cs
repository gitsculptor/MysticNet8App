using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using MysticNet8App.Contracts.Interfaces;

namespace MysticNet8App.Infrastructure.Persistance;

public abstract class RepositoryBase<T>(RepositoryContext repositoryContext) : IRepositoryBase<T>
    where T : class
{
    private readonly RepositoryContext _repositoryContext = repositoryContext ?? throw new ArgumentNullException(nameof(repositoryContext));

    public IQueryable<T> FindAll(bool trackChanges) =>
        !trackChanges
            ? _repositoryContext.Set<T>()
                .AsNoTracking()
            : _repositoryContext.Set<T>();

    public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression,
        bool trackChanges) =>
        !trackChanges
            ? _repositoryContext.Set<T>()
                .Where(expression)
                .AsNoTracking()
            : _repositoryContext.Set<T>()
                .Where(expression);

    public void Create(T entity) => _repositoryContext.Set<T>().Add(entity);
    public void Update(T entity) => _repositoryContext.Set<T>().Update(entity);
    public void Delete(T entity) => _repositoryContext.Set<T>().Remove(entity);
}