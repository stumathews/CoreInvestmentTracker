using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Text.RegularExpressions;

namespace Support
{

    public class DatabaseConnections
    {

        private string GetRdsConnectionString(IConfiguration configuration)
        {
            var appConfig = configuration;

            var dbname = appConfig["RDS_DB_NAME"];

            if (string.IsNullOrEmpty(dbname)) return null;
            
            return "Data Source=" + appConfig["RDS_HOSTNAME"] + ";Initial Catalog=" + dbname + ";User ID=" + appConfig["RDS_USERNAME"] + ";Password=" + appConfig["RDS_PASSWORD"] + ";";
        }

        private static string GetPostgressConnectionString(IConfiguration config)
        {
            string GetHerokuPgSqlConnectionString(IConfiguration configuration)
            {
                var uri = configuration["DATABASE_URL"];
                var r = Regex.Match(uri, @"postgres:\/\/(?<user>.+?):(?<password>.+)@(?<host>[^:]+):5432\/(?<db>.+)");
                string host = r.Groups["host"].Value;
                string username = r.Groups["user"].Value;
                string password = r.Groups["password"].Value;
                string database = r.Groups["db"].Value;

                var s = $"Host={host};Port=5432;Username={username};Password={password};Database={database};SslMode=Require";
                System.Console.Write($"Connecting to '{s}'");
                return s;
            }

            var appConfig = config;

            var localPgSql = "Host=localhost;Port=5432;Username=postgres;Password=sa;Database=Investments";

            //var connectionString = localPgSql;
            var connectionString =  string.IsNullOrEmpty(appConfig["DATABASE_URL"]) 
            ? localPgSql
            : GetHerokuPgSqlConnectionString(appConfig);
            return connectionString;
        }

        internal static void UseDatabase(DbContextOptionsBuilder options, IConfiguration config)
        {
             var devConnectionString = config.GetConnectionString("SqlExpress2017LocalHostConnection");
             options.UseNpgsql(GetPostgressConnectionString(config));
                //options.UseSqlServer(!HostingEnvironment.IsDevelopment()
                //    // Connect to amazon RDS database in production
                //    ? Support.Connections.GetRdsConnectionString() ?? devConnectionString // fallback to local if cant find rds but not useful really
                //                                                      // Connect to whatever defined database in config file while in development
                //    : devConnectionString);
        }
    }
}