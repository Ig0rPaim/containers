using Microsoft.Extensions.Configuration;

namespace teste_docker_api;

public static class GetConnection
{
    private static WebApplicationBuilder builder = WebApplication.CreateBuilder();
    public static string GetConnectionStringPostgres(){
        return  builder
        .Configuration
        .GetSection("ConnectionStrings")
        .GetValue<string>("postgresConnection")
        ?? throw new ArgumentNullException("Connection String not found!");
    }

}
