using Bank.Api.Common.Api;
using Bank.Core.Handlers;
using Bank.Core.Models;
using Bank.Core.Requests.Categories;
using Bank.Core.Responses;

namespace Bank.Api.Endpoints.Categories;

public class DeleteCategoryEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    
        => app.MapDelete("/{id}", HandleAsync)
         .WithName("Categories: Delete")
         .WithSummary("Exclui uma categoria")
         .WithDescription("Exclui uma categoria")
         .Produces<Response<Category?>>() ;      

  public static async Task<IResult> HandleAsync(
    ICategoryHandler handler,
    long id)
  { 
    var request = new DeleteCategoryRequest
    {
      Id = id,
      UserId = "test@balta.io"
    };
 
 var result = await handler.DeleteAsync(request);
     return result.IsSuccess
        ? TypedResults.Ok(result)
        : TypedResults.BadRequest(result);
  }
}
