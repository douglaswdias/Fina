﻿using System.Security.Claims;
using Fina.Api.Commom.Api;
using Fina.Core.Handlers;
using Fina.Core.Models;
using Fina.Core.Requests.Categories;
using Fina.Core.Responses;

namespace Fina.Api.Endpoint.Categories;

public class DeleteCategoryEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapDelete("/{id}", HandleAsync)
            .WithName("Categories: Delete")
            .WithSummary("Delete Category")
            .WithDescription("Delete Selected Category")
            .WithOrder(3)
            .Produces<Response<Category?>>();

    private static async Task<IResult> HandleAsync(ICategoryHandler handler, long id)
    {
        var request = new DeleteCategoryRequest
        {
            UserId = ApiConfiguration.UserId,
            Id = id
        };

        var result = await handler.DeleteAsync(request);
        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}