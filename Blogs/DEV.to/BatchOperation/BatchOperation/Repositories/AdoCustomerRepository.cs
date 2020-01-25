using BatchOperation.Models;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace BatchOperation.Repositories
{
    public abstract class AdoBaseRepository
    {
        public AdoBaseRepository(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public string ConnectionString { get; }

        public IDbConnection CreateConnection()
        {
            return new SqlConnection(ConnectionString);
        }
    }

    public class AdoCustomerRepository : AdoBaseRepository
    {
        public AdoCustomerRepository()
            : base(@"Server=.;Database=BatchDB;Integrated Security=SSPI;")
        { }

        public int BatchInsert(IEnumerable<Customer> customers)
        {
            var affectedRows = 0;
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = "INSERT INTO [dbo].[Customer] (Name, SSN, Address, CreatedUtc, ModifiedUtc) VALUES (@Name, @SSN, @Address, @CreatedUtc, @ModifiedUtc);";
                        command.Transaction = transaction;
                        foreach (var customer in customers)
                        {
                            command.Parameters.Clear();
                            command.Parameters.AddWithValue("@Name", customer.Name);
                            command.Parameters.AddWithValue("@SSN", customer.SSN);
                            command.Parameters.AddWithValue("@Address", customer.Address);
                            command.Parameters.AddWithValue("@CreatedUtc", customer.CreatedUtc);
                            command.Parameters.AddWithValue("@ModifiedUtc", customer.ModifiedUtc);
                            affectedRows += command.ExecuteNonQuery();
                        }
                    }
                    transaction.Commit();
                }
            }
            return affectedRows;
        }
    }
}
