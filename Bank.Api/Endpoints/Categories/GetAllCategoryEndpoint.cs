using Bank.Api.Common.Api;
using Bank.Core;
using Bank.Core.Handlers;
using Bank.Core.Models;
using Bank.Core.Requests.Categories;
using Bank.Core.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Bank.Api.Endpoints.Categories;

 public class GetAllCategoryEndpoint : IEndpoint
 {
       public static void Map(IEndpointRouteBuilder app)
    
        => app.MapGet("/", HandleAsync)
         .WithName("Categories: Get All")
         .WithSummary("Recupera todas categorias")
         .WithDescription("Recupera todas categorias")
         .Produces<PagedResponse<List<Category>?>>() ;      

  public static async Task<IResult> HandleAsync(
    ICategoryHandler handler,
    [FromQuery]int pageNumber = Configuration.DefaultPageNumber,
    [FromQuery]int pageSize = Configuration.DefaultPageSize)
  { 
    var request = new GetAllCategoriesRequest
    {
      PageNumber = pageNumber,
      PageSize = pageSize,
      UserId = "test@balta.io"
    };
 
 var result = await handler.GetAllAsync(request);
     return result.IsSuccess
        ? TypedResults.Ok(result.Data)
        : TypedResults.BadRequest(result.Data);
  }
}
 