using System.Text.Json;
using Newtonsoft.Json;
using Npgsql;
using teste_docker_api;

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

var client = new HttpClient();

app.MapGet("/user", async ()=>{
    try
    {
        return await GetUser.GetUserNameAsync(client);
    }
    catch(ArgumentException e)
    {
        throw new ArgumentNullException(e.Message);
    }
    catch(Exception e)
    {
        throw new Exception(e.Message, e);
    }
});



app.MapPost("/user", async()=>{
    try
    {
        var name = await GetUser.GetUserNameToDBAsync(client);
        using(var conn = new NpgsqlConnection(GetConnection.GetConnectionStringPostgres())){
            conn.Open();
            using (var command = new NpgsqlCommand("INSERT INTO users (nome) VALUES (@user_name)", conn))
            {
                command.Parameters.AddWithValue("user_name", name);
                await command.ExecuteNonQueryAsync();
            }
        }
    }
    catch(ArgumentNullException e)
    {
        throw new ArgumentNullException(e.Message);
    }
    catch(Exception e)
    {
        throw new Exception("Internal errors, try again");
    }


});

app.Run();