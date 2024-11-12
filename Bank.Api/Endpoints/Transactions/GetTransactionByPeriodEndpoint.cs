using System.Security.Claims;
using Bank.Api.Common.Api;
using Bank.Core;
using Bank.Core.Handlers;
using Bank.Core.Models;
using Bank.Core.Requests.Transactions;
using Bank.Core.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Bank.Api.Endpoints.Transactions;

public class GetTransactionByPeriodEndpoint : IEndpoint
{
  public static void Map(IEndpointRouteBuilder app)

      => app.MapGet("/", HandleAsync)
       .WithName("Transactions: Get All")
       .WithSummary("Recupera todas transações")
       .WithDescription("Recupera todas transações")
       .Produces<PagedResponse<List<Transaction>?>>();

  public static async Task<IResult> HandleAsync(
    ClaimsPrincipal user,
    ITransactionHandler handler,
    [FromQuery] DateTime? startDate = null,
    [FromQuery] DateTime? endDate = null,
    [FromQuery] int pageNumber = Configuration.DefaultPageNumber,
    [FromQuery] int pageSize = Configuration.DefaultPageSize)
  {
    var request = new GetTransactionByPeriodRequest
    {
      PageNumber = pageNumber,
      PageSize = pageSize,
      UserId = user.Identity?.Name ?? string.Empty,
      StartDate = startDate,
      EndDate = endDate,
    };

    var result = await handler.GetByPeriodAsync(request);
    return result.IsSuccess
       ? TypedResults.Ok(result)
       : TypedResults.BadRequest(result);
  }
}