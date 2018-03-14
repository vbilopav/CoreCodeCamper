using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Shared.Common.Interfaces
{
    public interface IAsyncReadRepository<T>
    {
        Task<T> GetByIdAsync<TParam>(TParam id);
        Task<List<T>> GetAllAsync();
        Task<List<T>> GetByAsync(ISpecification<T> spec);
    }

    public interface IAsyncUpdateRepository<T>
    {
        Task<T> AddAsync(T entity);
        Task UpdateAsync(T entity);
    }

    public interface ISpecification<T>
    {
        Expression<Func<T, bool>> Criteria { get; }
    }
}