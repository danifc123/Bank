using System.Security.Claims;
using Azure;
using Bank.Api.Common.Api;
using Bank.Core.Handlers;
using Bank.Core.Models;
using Bank.Core.Requests.Categories;

namespace Bank.Api.Endpoints.Categories;

public class CreateCategoryEndpoint : IEndpoint
{
  public static void Map(IEndpointRouteBuilder app)
  => app.MapPost("/", HandleAsync)
       .WithName("Categories: Create")
       .WithSummary("Cria uma nova categoria")
       .WithDescription("Cria uma categoria")
       .Produces<Response<Category?>>();

  private static async Task<IResult> HandleAsync(
    ClaimsPrincipal user,
    ICategoryHandler handler,
    CreateCategoryRequest request
  )
  {
    request.UserId = user.Identity?.Name ?? string.Empty;
    var result = await handler.CreateAsync(request);
    return result.IsSuccess
       ? TypedResults.Created($"/{result.Data?.Id}", result)
       : TypedResults.BadRequest(result);

  }
}