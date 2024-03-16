using System.Text.Json;
using Newtonsoft.Json;
using Npgsql;

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

app.MapGet("/user", async ()=>{
    var client  = new HttpClient();
    var response  = await client.GetAsync("https://randomuser.me/api");
    var jsonResponse = await response.Content.ReadFromJsonAsync<dynamic>();
    return jsonResponse;
});

string connString = "Host=postgres_server;Port=5432;Database=user_testes;Username=postgres;Password=1234";

app.MapPost("/user", async()=>{
    var client  = new HttpClient();
    var response  = await client.GetAsync("https://randomuser.me/api");
    var jsonString  = await response.Content.ReadAsStringAsync();
    var jsonDocument = JsonDocument.Parse(jsonString);
    var name = jsonDocument
    .RootElement
    .GetProperty("results")[0]
    .GetProperty("name")
    .GetProperty("first");
    using(var conn = new NpgsqlConnection(connString)){
        conn.Open();
        using (var command = new NpgsqlCommand("INSERT INTO users (nome) VALUES (@user_name)", conn))
        {
            command.Parameters.AddWithValue("user_name", name);
            await command.ExecuteNonQueryAsync();
        }
    }


});

app.Run();