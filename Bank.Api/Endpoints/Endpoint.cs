
using Bank.Api.Common.Api;
using Bank.Api.Endpoints.Categories;
using Bank.Api.Endpoints.Transactions;

namespace Bank.Api.Endpoints;

public static class Endpoint
{
  public static void MapEndpoints(this WebApplication app)
  {
    var endpoints = app
    .MapGroup("");

    endpoints.MapGroup("v1/categories")
    .WithTags("Categories")
    .MapEndpoint<CreateCategoryEndpoint>()
    .MapEndpoint<UpdateCategoryEndpoint>()
    .MapEndpoint<DeleteCategoryEndpoint>()
    .MapEndpoint<GetCategoryByIdEndpoint>()
    .MapEndpoint<GetAllCategoryEndpoint>();

    endpoints.MapGroup("v1/transactions")
  .WithTags("Transactions")
  .MapEndpoint<CreateTransactionEndpoint>()
  .MapEndpoint<UpdateTransactionEndpoint>()
  .MapEndpoint<DeleteTransactionEndpoint>()
  .MapEndpoint<GetTransactionByIdEndpoint>()
  .MapEndpoint<GetTransactionByPeriodEndpoint>();
  }
  private static IEndpointRouteBuilder MapEndpoint<TEndpoint>(this IEndpointRouteBuilder app)
      where TEndpoint : IEndpoint
  {
    TEndpoint.Map(app);
    return app;
  }
}