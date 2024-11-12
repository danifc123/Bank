using System.Security.Claims;
using Bank.Api.Common.Api;
using Bank.Core.Handlers;
using Bank.Core.Models;
using Bank.Core.Requests.Categories;
using Bank.Core.Responses;

namespace Bank.Api.Endpoints.Categories;

public class UpdateCategoryEndpoint : IEndpoint
{
  public static void Map(IEndpointRouteBuilder app)
 => app.MapPut("/{id}", HandleAsync)
      .WithName("Categories: Update")
      .WithSummary("Atualiza uma categoria")
      .WithDescription("Atualiza uma categoria")
      .Produces<Response<Category?>>();

  public static async Task<IResult> HandleAsync(
    ClaimsPrincipal user,
    ICategoryHandler handler,
    UpdateCategoryRequest request,
    long id)
  {
    request.UserId = user.Identity?.Name ?? string.Empty;
    request.Id = id;
    var result = await handler.UpdateAsync(request);
    return result.IsSuccess
       ? TypedResults.Ok(result)
       : TypedResults.BadRequest(result);
  }
}