using Client.DTO;
using Client.Requests;

namespace Client;

public class Program
{
    public const string Url = "https://localhost:7193/api/";

    public static void Main(string[] args)
    {
        GetRest();
    }

    public static async void GetRest()
    {
        var requests = new RestaurantRequests();

        var rest = new RestaurantModel() { Name = "rest1" };
        requests.Create(rest, Url);

        await requests.GetAllProductsAsync(Url);
    }
}