
using Client.DTO;
using System.Net.Http.Json;
using System.Text.Json;

namespace Client.Requests;

internal class RestaurantRequests
{
    internal async void Create(RestaurantModel restaurant, string url)
    {
        try
        {
            var createUrl = "https://localhost:7193/api/Restaurant/CreateRestaurant";

            var jsonSerializerOprions = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };

            using var httpClient = new HttpClient();

            var respose = await httpClient.PostAsJsonAsync(createUrl, restaurant);

            if (respose.IsSuccessStatusCode)
            {
                var numb = await respose.Content.ReadAsStringAsync();
            }
        }
        catch (Exception ex) { string t = ex.Message; }
    }

    public async Task<RestaurantModel> GetAllProductsAsync(string url)
    {
        var createUrl = "https://localhost:7193/api/Restaurant/GetRestaurant/1";
        var jsonSerializerOprions = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };

        using var httpClient = new HttpClient();

        var response = await httpClient.GetFromJsonAsync<RestaurantModel>(createUrl);
        //var products = JsonSerializer.Deserialize<List<ProductsDTO>>(response);
        return response;
    }
}
