using System.Net.Http.Json;
using Fina.Core.Handlers;
using Fina.Core.Models;
using Fina.Core.Requests.Transactions;
using Fina.Core.Responses;

namespace Fina.Web.Handlers;

public class TransactionHandler(IHttpClientFactory httpClientFactory) : ITransactionHandler
{
    private readonly HttpClient _http = httpClientFactory.CreateClient(WebConfiguration.HttpClientName);
    public async Task<Response<Transaction?>> CreateAsync(CreateTransactionRequest request)
    {
        var result = await _http.PostAsJsonAsync("v1/transactions", request);
        return await result.Content.ReadFromJsonAsync<Response<Transaction?>>()
        ?? new Response<Transaction?>(null, 400, "Falha ao criar transação");
    }

    public async Task<Response<Transaction?>> UpdateAsync(UpdateTransactionRequest request)
    {
        throw new NotImplementedException();
    }

    public async Task<Response<Transaction?>> DeleteAsync(DeleteTransactionRequest request)
    {
        throw new NotImplementedException();
    }

    public async Task<Response<Transaction?>> GetByIdAsync(GetTransactionByIdRequest request)
    {
        throw new NotImplementedException();
    }

    public async Task<PagedResponse<List<Transaction?>>> GetByDateAsync(GetTransactionByDateRequest request)
    {
        throw new NotImplementedException();
    }
}