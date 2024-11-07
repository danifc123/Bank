
using Bank.Api.Common.Api;
using Bank.Api.Endpoints.Categories;

namespace Bank.Api.Endpoints;

public static class Endpoint
{
  public static void MapEndpoints(this WebApplication app)
  {
     var endpoints = app
     .MapGroup("");

     endpoints.MapGroup("v1/categories")
     .WithTags("Categories")
     .MapEndpoint<CreateCategoryEndpoint>();
  }
  private static IEndpointRouteBuilder MapEndpoint<TEndpoint>(this IEndpointRouteBuilder app) 
      where TEndpoint : IEndpoint
      {
        TEndpoint.Map(app);
        return app;
      }
}