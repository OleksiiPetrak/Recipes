using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace RecipesBook.Core.Interfaces
{
    public interface IRepositoryAsync<T> where T : class, new()
    {
        Task UpsertOneAsync(T item);
        Task UpsertManyAsync(List<T> items);
        Task<T> GetOneAsync(Expression<Func<T, bool>> predicate);
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);
        Task<IEnumerable<T>> GetAllAsync();
        Task DeleteAsync(T item);
    }
}
