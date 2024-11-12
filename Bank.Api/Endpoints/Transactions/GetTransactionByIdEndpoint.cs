using System.Security.Claims;
using Bank.Api.Common.Api;
using Bank.Core.Handlers;
using Bank.Core.Models;
using Bank.Core.Requests.Transactions;
using Bank.Core.Responses;

namespace Bank.Api.Endpoints.Transactions;

public class GetTransactionByIdEndpoint : IEndpoint
{
  public static void Map(IEndpointRouteBuilder app)

      => app.MapGet("/{id}", HandleAsync)
       .WithName("Transactions: Get By Id")
       .WithSummary("Recupera uma transação")
       .WithDescription("Recupera uma transação")
       .Produces<Response<Transaction?>>();

  public static async Task<IResult> HandleAsync(
    ClaimsPrincipal user,
    ITransactionHandler handler,
    long id)
  {
    var request = new GetTransactionByIdRequest
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