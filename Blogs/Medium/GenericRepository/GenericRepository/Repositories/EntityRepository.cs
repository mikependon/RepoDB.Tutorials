using RepoDb;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace GenericRepository.Repositories
{
    public abstract class EntityRepository<TEntity, TDbConnection>
        where TEntity : class
        where TDbConnection : IDbConnection
    {
        public EntityRepository(string connectionString)
        {
            ConnectionString = connectionString;
        }

        /* Properties */

        public string ConnectionString { get; }

        /* Helpers */

        public IDbConnection GetConnection()
        {
            var connection = Activator.CreateInstance<TDbConnection>();
            connection.ConnectionString = ConnectionString;
            return connection;
        }

        /* Methods */

        public virtual async Task<int> DeleteAsync(TEntity entity)
        {
            using (var connection = GetConnection())
            {
                return await connection.DeleteAsync<TEntity>(entity);
            }
        }

        public virtual async Task<TEntity> GetAsync(object id)
        {
            using (var connection = GetConnection())
            {
                return (await connection.QueryAsync<TEntity>(id)).FirstOrDefault();
            }
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync(string cacheKey = null)
        {
            using (var connection = GetConnection())
            {
                return await connection.QueryAllAsync<TEntity>(cacheKey: cacheKey);
            }
        }

        public virtual async Task<TReturn> MergeAsync<TReturn>(TEntity entity)
        {
            using (var connection = GetConnection())
            {
                return await connection.MergeAsync<TEntity, TReturn>(entity);
            }
        }

        public virtual async Task<TReturn> SaveAsync<TReturn>(TEntity entity)
        {
            using (var connection = GetConnection())
            {
                return await connection.InsertAsync<TEntity, TReturn>(entity);
            }
        }

        public virtual async Task<int> UpdateAsync(TEntity entity)
        {
            using (var connection = GetConnection())
            {
                return await connection.UpdateAsync<TEntity>(entity);
            }
        }

        /* Batch Operations */

        public virtual async Task<int> DeleteAllAsync(IEnumerable<TEntity> entities)
        {
            using (var connection = GetConnection())
            {
                return await connection.DeleteAllAsync<TEntity>(entities);
            }
        }

        public virtual async Task<int> MergeAllAsync(IEnumerable<TEntity> entities)
        {
            using (var connection = GetConnection())
            {
                return await connection.MergeAllAsync<TEntity>(entities);
            }
        }

        public virtual async Task<int> SaveAllAsync(IEnumerable<TEntity> entities)
        {
            using (var connection = GetConnection())
            {
                return await connection.InsertAllAsync<TEntity>(entities);
            }
        }

        public virtual async Task<int> UpdateAllAsync(IEnumerable<TEntity> entities)
        {
            using (var connection = GetConnection())
            {
                return await connection.UpdateAllAsync<TEntity>(entities);
            }
        }
    }
}
