using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace AvaloniaApplication1.Models.PokeApi;

public class PokeApi
{
    private HttpClient _client;

    public PokeApi()
    {
        this._client = new HttpClient();
        this._client.BaseAddress = new Uri("https://pokeapi.co/api/v2/");
    }

    public async Task<string> GetPokemonData(string pokemonName)
    {
        return await DoRequestApi("pokemon/" + pokemonName);
    }

    private async Task<string> DoRequestApi(string endpoint)
    {
        string responseData = "";

        try {
        using HttpResponseMessage response = await this._client.GetAsync(endpoint);

        if (response.IsSuccessStatusCode)
        {

            responseData = await response.Content.ReadAsStringAsync();
        }
        else
        {
            responseData = $"{response.StatusCode}";
            Console.WriteLine($"Erreur lors de la requête : {response.StatusCode}");
        }
        } catch (Exception ex)
        {
            responseData = "{ex.Message}";
            Console.WriteLine($"Exception : {ex.Message}");
        }

        return responseData;
    }
}