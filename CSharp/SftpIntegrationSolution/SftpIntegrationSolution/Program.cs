using CsvHelper;
using Hangfire;
using Hangfire.MemoryStorage;
using Microsoft.Data.SqlClient;
using Renci.SshNet;
using RepoDb;
using RepoDb.Enumerations;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.IO.Compression;

namespace SftpIntegrationSolution
{
    class Program
    {
        static void Main(string[] args)
        {
            Bootstrap();
            var data = QueryData(0);
            var csvPath = SaveCsv(data);
            var zipPath = Compress(csvPath);
            UploadToSftp(zipPath);
            File.Delete(csvPath);
            File.Delete(zipPath);
        }

        static void EnsureDirectory(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        static void Bootstrap()
        {
            SqlServerBootstrap.Initialize();
            JobStorage.Current = new MemoryStorage();
        }

        static IEnumerable<dynamic> QueryData(long lastId)
        {
            using (var connection = new SqlConnection("Server=(local);Database=Test;Integrated Security=SSPI;"))
            {
                var where = new QueryField("Id", Operation.GreaterThan, lastId);
                return connection.Query("Person", where);
            }
        }

        static string SaveCsv(IEnumerable<dynamic> data)
        {
            var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "CSV");
            EnsureDirectory(path);
            var fileName = Path.Combine(path, $"Person-{DateTime.UtcNow.ToString("yyyyMMddHHmmss")}.csv");
            using (var writer = new StreamWriter(fileName))
            {
                using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                {
                    csv.WriteRecords(data);
                }
            }
            return path;
        }

        static string Compress(string path)
        {
            var zipPath = $"{path}.zip";
            ZipFile.CreateFromDirectory(path, zipPath);
            return zipPath;
        }

        static void UploadToSftp(string zipPath)
        {
            var connectionInfo = new ConnectionInfo("SftpServer",
                   "Username",
                   new PasswordAuthenticationMethod("Username", "Password"),
                   new PrivateKeyAuthenticationMethod("rsa.key"));

            using (var sftpClient = new SftpClient(connectionInfo))
            {
                using (var stream = new FileStream(zipPath, FileMode.Open, FileAccess.Read))
                {
                    var previousValue = (double)0;
                    sftpClient.UploadFile(stream, zipPath, (value) =>
                    {
                        var percentage = ((double)value / stream.Length) * 100;
                        var megaBytes = (double)value / (1024 * 1024);
                        var dividend = Math.Floor(percentage / 5);
                        if (dividend > previousValue)
                        {
                            previousValue = dividend;
                            Console.WriteLine($"\n\t--> Completed: {percentage.ToString("##0.00")}% ({megaBytes.ToString("###0.00")} MB)");
                        }
                    });
                }
            }
        }
    }
}
