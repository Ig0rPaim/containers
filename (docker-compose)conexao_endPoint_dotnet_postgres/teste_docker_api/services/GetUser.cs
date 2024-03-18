using System.Text.Json;

namespace teste_docker_api;

public static class GetUser
{
    public static async Task<string> GetUserNameToDBAsync(HttpClient client){
        try
        {
            var response  = await client.GetAsync("https://randomuser.me/api");
            var jsonString  = await response.Content.ReadAsStringAsync() 
            ?? throw new ArgumentNullException("Error when getting user name");
            var jsonDocument = JsonDocument.Parse(jsonString);
            return jsonDocument
            .RootElement
            .GetProperty("results")[0]
            .GetProperty("name")
            .GetProperty("first")
            .ToString();
        }
        catch(Exception e)
        {
            throw new Exception(e.Message, e);
        }
    }

    public static async Task<dynamic> GetUserNameAsync(HttpClient client)
    {
        try
        {
            var response  = await client.GetAsync("https://randomuser.me/api");
            var jsonResponse = await response.Content.ReadFromJsonAsync<dynamic>();
            return jsonResponse ?? throw new ArgumentNullException("Error when getting user name");
        }
        catch(ArgumentNullException e)
        {
            throw new ArgumentNullException(e.Message);
        }
        catch(Exception e)
        {
            throw new Exception(e.Message, e);
        }
    }
}
