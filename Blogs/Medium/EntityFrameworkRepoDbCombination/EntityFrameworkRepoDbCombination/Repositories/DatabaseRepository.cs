using EntityFrameworkRepoDbCombination.Cachers;
using Microsoft.Data.SqlClient;
using RepoDb;

namespace EntityFrameworkRepoDbCombination.Repositories
{
    public class DatabaseRepository : DbRepository<SqlConnection>
    {
        public DatabaseRepository() :
            base("Server=PC79000;Database=TestDB;Integrated Security=SSPI;", cache: CacheFactory.Create())
        { }
    }
}
