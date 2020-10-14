using CsvHelper;
using Hangfire;
using Hangfire.MemoryStorage;
using Microsoft.Data.SqlClient;
using Renci.SshNet;
using RepoDb;
using RepoDb.Enumerations;
using RepoDb.Extensions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Linq;

namespace SftpIntegrationSolution
{
    class Program
    {
        private static readonly string connectionString =
            "Server=(local);Database=Test;Integrated Security=SSPI;";

        static void Main(string[] args)
        {
            Bootstrap();
            ConfigureSchedules();
            Console.ReadLine();
        }

        public static void Process()
        {
            var folder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "CSV",
                $"Data-{DateTime.UtcNow.ToString("yyyyMMddHHmmss")}");
            var tables = GetTables();

            foreach (var table in tables)
            {
                var data = QueryTable(table.TableName, table.ColumnName, table.Value);
                var csvPath = SaveCsv(folder, table.TableName, data);
                Console.WriteLine($"The CSV '{csvPath}' has been created.");
                var maxId = GetMaxId(data, table.ColumnName);
                SaveLastValue(table.TableName, maxId);
            }

            var zipPath = Compress(folder);
            UploadToSftp(zipPath);
            File.Delete(zipPath);
        }

        static void Bootstrap()
        {
            SqlServerBootstrap.Initialize();
            JobStorage.Current = new MemoryStorage();
        }

        static void EnsureDirectory(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        static IEnumerable<dynamic> GetTables()
        {
            using (var connection = new SqlConnection(connectionString))
            {
                return connection.QueryAll("Status", hints: SqlServerTableHints.NoLock);
            }
        }

        static IEnumerable<dynamic> QueryTable(string tableName,
            string columnName,
            object lastValue)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var where = new QueryField(columnName, Operation.GreaterThan, lastValue);
                var orderBy = new OrderField(columnName, Order.Ascending);
                var top = 100;
                return connection.Query(tableName, where, top: top,
                    orderBy: orderBy.AsEnumerable(), hints: SqlServerTableHints.NoLock);
            }
        }

        static string SaveCsv(string folder,
            string tableName,
            IEnumerable<dynamic> data)
        {
            EnsureDirectory(folder);
            var fileName = Path.Combine(folder, $"{tableName}.csv");
            using (var writer = new StreamWriter(fileName))
            {
                using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                {
                    csv.WriteRecords(data);
                }
            }
            return fileName;
        }

        static string Compress(string path)
        {
            var zipPath = $"{path}.zip";
            ZipFile.CreateFromDirectory(path, zipPath);
            return zipPath;
        }

        static void UploadToSftp(string zipPath)
        {
            var connectionInfo = new ConnectionInfo("sftp.company.com",
                "SftpUser",
                new PasswordAuthenticationMethod("SftpUser", "SftpPassword"),
                new PrivateKeyAuthenticationMethod("rsa.key"));

            using (var sftpClient = new SftpClient(connectionInfo))
            {
                sftpClient.Connect();
                using (var stream = new FileStream(zipPath, FileMode.Open, FileAccess.Read))
                {
                    var directory = "\\TopLevelFolder\\Data";
                    if (sftpClient.Exists(directory) == false)
                    {
                        sftpClient.CreateDirectory(directory);
                    }
                    var fileName = Path.Combine(directory, Path.GetFileName(zipPath));
                    var previousValue = (double)0;
                    sftpClient.UploadFile(stream, fileName, (value) =>
                    {
                        WriteProgress(value, stream.Length, ref previousValue);
                    });
                }
            }
        }

        static void WriteProgress(ulong value,
            long streamLength,
            ref double previousValue)
        {
            var percentage = ((double)value / streamLength) * 100;
            var megaBytes = (double)value / (1024 * 1024);
            var dividend = Math.Floor(percentage / 5);
            if (dividend > previousValue)
            {
                previousValue = dividend;
                Console.WriteLine($"\n\t--> Completed: {percentage.ToString("##0.00")}% " +
                    $"({megaBytes.ToString("###0.00")} MB)");
            }
        }

        static void SaveLastValue(string tableName,
            object value)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Merge("Status", new { tableName, value }, qualifiers: Field.From("TableName"));
            }
        }

        static object GetMaxId(IEnumerable<dynamic> data,
            string columnName)
        {
            var kvps = data?.OfType<IDictionary<string, object>>();
            return kvps.Max(e => e[columnName]);
        }

        static void ConfigureSchedules()
        {
            RecurringJob.AddOrUpdate("UploadToSftp", () => Process(), Cron.Minutely());
        }
    }
}
