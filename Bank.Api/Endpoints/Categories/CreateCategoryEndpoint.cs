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
         .Produces<Response<Category?>>() ;      

    private static async Task<IResult> HandleAsync(
      ICategoryHandler handler,
      CreateCategoryRequest request
    )
    {
      var result = await handler.CreateAsync(request);
     return result.IsSuccess
        ? TypedResults.Created($"/{result.Data?.Id}",result.Data)
        : TypedResults.BadRequest(result.Data);
        
    }
}