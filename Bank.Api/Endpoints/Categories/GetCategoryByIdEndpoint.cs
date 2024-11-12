using System.Security.Claims;
using Bank.Api.Common.Api;
using Bank.Core.Handlers;
using Bank.Core.Models;
using Bank.Core.Requests.Categories;
using Bank.Core.Responses;

namespace Bank.Api.Endpoints.Categories;

public class GetCategoryByIdEndpoint : IEndpoint
{
  public static void Map(IEndpointRouteBuilder app)

         => app.MapGet("/{id}", HandleAsync)
          .WithName("Categories: Get By Id")
          .WithSummary("Recupera uma categoria")
          .WithDescription("Recupera uma categoria")
          .Produces<Response<Category?>>();

  public static async Task<IResult> HandleAsync(
    ClaimsPrincipal user,
    ICategoryHandler handler,
    long id)
  {
    var request = new GetCategoriesByIdRequest
    {
      Id = id,
      UserId = user.Identity?.Name ?? string.Empty

    };

    var result = await handler.GetByIdAsync(request);
    return result.IsSuccess
       ? TypedResults.Ok(result)
       : TypedResults.BadRequest(result);
  }
}