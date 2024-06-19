using System.Net.Http.Json;
using Fina.Core.Handlers;
using Fina.Core.Models;
using Fina.Core.Requests.Categories;
using Fina.Core.Responses;

namespace Fina.Web.Handlers;

public class CategoryHandler(IHttpClientFactory httpClientFactory) : ICategoryHandler
{
    private readonly HttpClient _http = httpClientFactory.CreateClient(WebConfiguration.HttpClientName);
    public async Task<Response<Category?>> CreateAsync(CreateCategoryRequest request)
    {
        var result = await _http.PostAsJsonAsync("v1/categories", request);
        return await result.Content.ReadFromJsonAsync<Response<Category?>>() 
            ?? new Response<Category?>(null, 400, "Falha ao criar categoria");
    }

    public async Task<Response<Category?>> UpdateAsync(UpdateCategoryRequest request)
    {
        var result = await _http.PutAsJsonAsync($"v1/categories/{request.Id}", request);
        return await result.Content.ReadFromJsonAsync<Response<Category?>>() 
            ?? new Response<Category?>(null, 400, "Falha ao carregar categoria");
    }

    public async Task<Response<Category?>> DeleteAsync(DeleteCategoryRequest request)
    {
        var result = await _http.DeleteAsync($"v1/categories/{request.Id}");
        return await result.Content.ReadFromJsonAsync<Response<Category?>>() 
            ?? new Response<Category?>(null, 400, "Falha ao apagar categoria");
    }

    public async Task<Response<Category?>> GetByIdAsync(GetCategoryByIdRequestt request)
        => await _http.GetFromJsonAsync<Response<Category?>>($"v1/categories/{request.Id}") 
        ?? new Response<Category?>(null, 400, "Falha ao carregar categoria");

    public async Task<PagedResponse<List<Category?>>> GetAllAsync(GetAllCategoriesRequest request)
        => await _http.GetFromJsonAsync<PagedResponse<List<Category?>>>($"v1/categories") 
        ?? new PagedResponse<List<Category?>>(null, 400, "Falha ao carregar categoria");
}