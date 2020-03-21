using RecipesBook.Core.Interfaces;
using RecipesBook.Core.Models;
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
            connection.CreateTableAsync<T>().Wait();
        }

        public async Task<T> GetOneAsync(Expression<Func<T, bool>> predicate)
        {
            return await _connection.Table<T>().FirstOrDefaultAsync(predicate).ConfigureAwait(false);
        }

        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
        {
            return await _connection.Table<T>().Where(predicate).ToListAsync().ConfigureAwait(false);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            try
            {
                var result = await _connection.Table<T>().ToListAsync().ConfigureAwait(false);
                return result;
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        public async Task UpsertOneAsync(T item)
        {
            await _connection.InsertOrReplaceAsync(item).ConfigureAwait(false);
        }

        public async Task UpsertManyAsync(List<T> items)
        {
            foreach (var item in items)
            {
                await _connection.InsertOrReplaceAsync(item).ConfigureAwait(false);
            }
        }

        public async Task DeleteAsync(T item)
        {
            await _connection.DeleteAsync(item).ConfigureAwait(false);
        }
    }
}
