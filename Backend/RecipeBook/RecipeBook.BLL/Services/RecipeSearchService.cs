using System.Net.Http.Json;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using RecipeBook.BLL.Models.DTOs.Search;
using RecipeBook.BLL.Services.Interfaces;

namespace RecipeBook.BLL.Services;

public class RecipeSearchService : IRecipeSearchService
{
    private readonly IMapper _mapper;
    private readonly HttpClient _client;

    public RecipeSearchService(IMapper mapper, IConfiguration configuration)
    {
        _mapper = mapper;
        _client = new HttpClient();
        _client.DefaultRequestHeaders.Add("X-Api-Key", configuration["Secrets:X-Api-Key"]);
    }

    public async Task<IEnumerable<RecipeDisplayDTO>> SearchAsync(string title)
    {
        var response = await _client.GetAsync(GetRequestUri(title));
        //put into try catch with custom Exception
        response.EnsureSuccessStatusCode();
        var searchResults = await response.Content.ReadFromJsonAsync<IEnumerable<RecipeSearchDTO>>();
        return _mapper.Map<IEnumerable<RecipeDisplayDTO>>(searchResults);
    }

    private string GetRequestUri(string title) => $"https://api.api-ninjas.com/v1/recipe?query={title}";
}