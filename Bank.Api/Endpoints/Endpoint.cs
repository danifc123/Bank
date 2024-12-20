
using Bank.Api.Common.Api;
using Bank.Api.Endpoints.Categories;
using Bank.Api.Endpoints.Identity;
using Bank.Api.Endpoints.Transactions;
using Bank.Api.Models;

namespace Bank.Api.Endpoints;

public static class Endpoint
{
  public static void MapEndpoints(this WebApplication app)
  {
    var endpoints = app
    .MapGroup("");

    endpoints.MapGroup("/")
    .WithTags("Health Check")
    .MapGet("/", () => new { message = "ok" });

    endpoints.MapGroup("v1/categories")
    .WithTags("Categories")
    .RequireAuthorization()
    .MapEndpoint<CreateCategoryEndpoint>()
    .MapEndpoint<UpdateCategoryEndpoint>()
    .MapEndpoint<DeleteCategoryEndpoint>()
    .MapEndpoint<GetCategoryByIdEndpoint>()
    .MapEndpoint<GetAllCategoryEndpoint>();

    endpoints.MapGroup("v1/transactions")
  .WithTags("Transactions")
  .RequireAuthorization()
  .MapEndpoint<CreateTransactionEndpoint>()
  .MapEndpoint<UpdateTransactionEndpoint>()
  .MapEndpoint<DeleteTransactionEndpoint>()
  .MapEndpoint<GetTransactionByIdEndpoint>()
  .MapEndpoint<GetTransactionByPeriodEndpoint>();

    endpoints.MapGroup("v1/identity")
      .WithTags("Identity")
      .MapIdentityApi<User>();


    endpoints.MapGroup("v1/identity")
    .WithTags("Identity")
    .MapEndpoint<LogoutEndpoint>()
    .MapEndpoint<GetRolesEndpoints>();

  }
  private static IEndpointRouteBuilder MapEndpoint<TEndpoint>(this IEndpointRouteBuilder app)
      where TEndpoint : IEndpoint
  {
    TEndpoint.Map(app);
    return app;
  }
}