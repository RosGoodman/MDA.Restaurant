using Newtonsoft.Json;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace Client;

public class Program
{
    public static void Main(string[] args)
    {

        //new Program().GetTable();
        new Program().CreateTable();
    }

    private async void CreateTable()
    {
        using var client = new HttpClient();
        var url = "https://localhost:7193/api/Table/CreateTable";
        var jsonSerializerOptions = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };

        var newTable = new TableModel()
        {
            RestaurantModelId = 2,
            SeatsCount = 8,
            State = State.Free
        };

        var result = await client.PostAsJsonAsync(url, newTable);
        if (result.IsSuccessStatusCode)
        {
            var content = await result.Content.ReadAsStringAsync();
            Console.WriteLine(content);
        }
    }

    private void GetTable()
    {
        using var client = new HttpClient();
        var endpoint = new Uri("https://localhost:7193/api/Table/GetTable?id=1");

        var result = client.GetAsync(endpoint).Result;
        var json = result.Content.ReadAsStringAsync().Result;
    }
}