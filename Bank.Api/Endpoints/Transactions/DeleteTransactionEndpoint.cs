using Bank.Api.Common.Api;
using Bank.Core.Handlers;
using Bank.Core.Models;
using Bank.Core.Requests.Transactions;
using Bank.Core.Responses;

namespace Bank.Api.Endpoints.Transactions;

public class DeleteTransactionEndpoint : IEndpoint
{
  public static void Map(IEndpointRouteBuilder app)

        => app.MapDelete("/{id}", HandleAsync)
         .WithName("Transactions: Delete")
         .WithSummary("Exclui uma transação")
         .WithDescription("Exclui uma transação")
         .Produces<Response<Transaction?>>();

  public static async Task<IResult> HandleAsync(
    ITransactionHandler handler,
    long id)
  {
    var request = new DeleteTransactionRequest
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