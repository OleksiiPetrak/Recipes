using RecipesBook.Core.Interfaces;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace RecipesBook.Core.Repositories
{
    public class GenericRepositoryAsync<T> : IRepositoryAsync<T> where T : class, new()
    {
        private readonly SQLiteAsyncConnection _connection;

        public GenericRepositoryAsync(SQLiteAsyncConnection connection)
        {
            _connection = connection;
            connection.CreateTableAsync<T>();
        }

        public async Task<T> GetOneAsync(Expression<Func<T, bool>> predicate)
        {
            return await _connection.Table<T>().FirstOrDefaultAsync(predicate);
        }

        public async Task<IQueryable<T>> FindAsync(Expression<Func<T, bool>> predicate)
        {
            return await _connection.Table<T>().Where(predicate).ToListAsync() as IQueryable<T>;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _connection.Table<T>().ToListAsync();
        }

        public async Task UpsertOneAsync(T item)
        {
            await _connection.InsertOrReplaceAsync(item);
        }

        public async Task UpsertManyAsync(List<T> items)
        {
            foreach (var item in items)
            {
                await _connection.InsertOrReplaceAsync(item);
            }
        }

        public async Task DeleteAsync(T item)
        {
            await _connection.DeleteAsync(item);
        }
    }
}
