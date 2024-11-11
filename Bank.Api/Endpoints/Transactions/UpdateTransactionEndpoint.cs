using Bank.Api.Common.Api;
using Bank.Core.Handlers;
using Bank.Core.Models;
using Bank.Core.Requests.Transactions;
using Bank.Core.Responses;

namespace Bank.Api.Endpoints.Transactions;

public class UpdateTransactionEndpoint : IEndpoint
{

  public static void Map(IEndpointRouteBuilder app)
 => app.MapPut("/{id}", HandleAsync)
      .WithName("Transactions: Update")
      .WithSummary("Atualiza uma transação")
      .WithDescription("Atualiza uma transação")
      .Produces<Response<Transaction?>>();

  public static async Task<IResult> HandleAsync(
    ITransactionHandler handler,
    UpdateTransactionRequest request,
    long id)
  {
    request.UserId = "test@balta.io";
    request.Id = id;
    var result = await handler.UpdateAsync(request);
    return result.IsSuccess
       ? TypedResults.Ok(result)
       : TypedResults.BadRequest(result);
  }
}