using Hospital_API.Data;
using Microsoft.EntityFrameworkCore;

namespace Hospital_API.Extensions;

public static class ServiceExtensions
{
    /// <summary>
    /// Connecting to the Microsoft SQL Database.
    /// </summary>
    public static IServiceCollection AddMsSQLDatabase(this IServiceCollection services, ConfigurationManager configuration, ILogger logger)
    {
        /* 
          The host, name, and password must be specified in the docker-compose.yml configuration file

          web-api:
            environment:
             - DB_HOST=host
             - DB_NAME=name
             - DB_USER=login
             - DB_SA_PASSWORD=password
        */

        string? dbHost = Environment.GetEnvironmentVariable("DB_HOST");
        string? dbName = Environment.GetEnvironmentVariable("DB_NAME");
        string? login = Environment.GetEnvironmentVariable("DB_USER");
        string? password = Environment.GetEnvironmentVariable("DB_PASSWORD");


        services.AddDbContext<AppDBContext>(opt =>
        {
            if (string.IsNullOrEmpty(dbHost) || string.IsNullOrEmpty(dbName) || string.IsNullOrEmpty(password))
            {
                opt.UseSqlServer(configuration.GetConnectionString("LocalDB"));
                logger.LogInformation("The database is loaded from Local.");
            }
            else
            {
                opt.UseSqlServer($"Server={dbHost};Database={dbName};User Id={login};Password={password};TrustServerCertificate=True;");
                logger.LogInformation("The database is loaded from Container.");
            }            
        });

        return services;
    }
}