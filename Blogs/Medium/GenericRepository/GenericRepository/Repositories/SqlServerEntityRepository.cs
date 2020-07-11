using Microsoft.Data.SqlClient;
using RepoDb;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GenericRepository.Repositories
{
    public abstract class SqlServerEntityRepository<TEntity> : EntityRepository<TEntity, SqlConnection>
        where TEntity : class
    {
        public SqlServerEntityRepository(string connectionString) :
            base(connectionString)
        { }

        /* Bulk */

        public override async Task<int> DeleteAllAsync(IEnumerable<TEntity> entities)
        {
            if (entities?.Count() > 1000)
            {
                using (var connection = (SqlConnection)GetConnection())
                {
                    return await connection.BulkDeleteAsync<TEntity>(entities);
                }
            }
            return await base.DeleteAllAsync(entities);
        }

        public override async Task<int> MergeAllAsync(IEnumerable<TEntity> entities)
        {
            if (entities?.Count() > 1000)
            {
                using (var connection = (SqlConnection)GetConnection())
                {
                    return await connection.BulkMergeAsync<TEntity>(entities);
                }
            }
            return await base.MergeAllAsync(entities);
        }

        public override async Task<int> SaveAllAsync(IEnumerable<TEntity> entities)
        {
            if (entities?.Count() > 1000)
            {
                using (var connection = (SqlConnection)GetConnection())
                {
                    return await connection.BulkInsertAsync<TEntity>(entities);
                }
            }
            return await base.SaveAllAsync(entities);
        }

        public override async Task<int> UpdateAllAsync(IEnumerable<TEntity> entities)
        {
            if (entities?.Count() > 1000)
            {
                using (var connection = (SqlConnection)GetConnection())
                {
                    return await connection.UpdateAllAsync<TEntity>(entities);
                }
            }
            return await base.UpdateAllAsync(entities);
        }
    }
}
