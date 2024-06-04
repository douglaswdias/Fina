using Fina.Api.Commom.Api;
using Fina.Core.Handlers;
using Fina.Core.Models;
using Fina.Core.Requests.Categories;
using Fina.Core.Responses;
using Microsoft.AspNetCore.SignalR;

namespace Fina.Api.Endpoint.Categories;

public class GetCategoryByIdEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/{id}", HandleAsync)
            .WithName("Categories: Get By Id")
            .WithSummary("Get One Category")
            .WithDescription("Get one category by id")
            .WithOrder(4)
            .Produces<Response<Category?>>();
    private static async Task<IResult> HandleAsync(ICategoryHandler handler, long id)
    {
        var request = new GetCategoryByIdRequestt
        {
            UserId = ApiConfiguration.UserId,
            Id = id
        };

        var result = await handler.GetByIdAsync(request);
        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}