using System.Security.Claims;
using Bank.Api.Common.Api;
using Bank.Core.Handlers;
using Bank.Core.Models;
using Bank.Core.Requests.Transactions;
using Bank.Core.Responses;

namespace Bank.Api.Endpoints.Transactions;

public class CreateTransactionEndpoint : IEndpoint
{

  public static void Map(IEndpointRouteBuilder app)
  => app.MapPost("/", HandleAsync)
       .WithName("Transactions: Create")
       .WithSummary("Cria uma nova transação")
       .WithDescription("Cria uma transação")
       .Produces<Response<Transaction?>>();

  private static async Task<IResult> HandleAsync(
    ClaimsPrincipal user,
    ITransactionHandler handler,
    CreateTransactionRequest request
  )
  {
    request.UserId = user.Identity?.Name ?? string.Empty;
    var result = await handler.CreateAsync(request);
    return result.IsSuccess
       ? TypedResults.Created($"/{result.Data?.Id}", result)
       : TypedResults.BadRequest(result);

  }
}