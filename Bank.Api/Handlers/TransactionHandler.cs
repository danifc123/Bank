

using Bank.Api.Data;
using Bank.Core.Common.Extensions;
using Bank.Core.Handlers;
using Bank.Core.Models;
using Bank.Core.Requests.Transactions;
using Bank.Core.Responses;
using Microsoft.EntityFrameworkCore;

namespace Bank.Api.Handlers;

public class TransactionHandler(AppDbContext context) : ITransactionHandler
{
  public async Task<Response<Transaction?>> CreateAsync(CreateTransactionRequest request)
  {
    try
    {
      var transaction = new Transaction
      {
        UserId = request.UserId,
        CategoryId = request.CategoryId,
        CreatedAt = DateTime.Now,
        Amount = request.Amount,
        PaidOrReceivedAt = request.PaidOrReceivedAt,
        Title = request.Title,
        Type = request.Type
      };
      await context.Transactions.AddAsync(transaction);
      await context.SaveChangesAsync();

      return new Response<Transaction?>(transaction, 201, "Transação criada com sucesso");

    }
    catch
    {
      return new Response<Transaction?>(null, 500, "Não foi possivel criar a transação");
    }
  }

  public async Task<Response<Transaction?>> UpdateAsync(UpdateTransactionRequest request)
  {
    try
    {
      var transaction = await context
      .Transactions
      .FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);

      if (transaction is null)
        return new Response<Transaction?>(null, 404, "Transação não encontrada");


      transaction.CategoryId = request.CategoryId;
      transaction.Amount = request.Amount;
      transaction.Title = request.Title;
      transaction.Type = request.Type;
      transaction.PaidOrReceivedAt = request.PaidOrReceivedAt;

      context.Transactions.Update(transaction);
      await context.SaveChangesAsync();

      return new Response<Transaction?>(transaction);

    }
    catch
    {
      return new Response<Transaction?>(null, 500, "Não foi possivel buscar a transação");
    }
  }
  public async Task<Response<Transaction?>> DeleteAsync(DeleteTransactionRequest request)
  {
    try
    {
      var transaction = await context
      .Transactions
      .FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);

      if (transaction is null)
        return new Response<Transaction?>(null, 404, "Transação não encontrada");


      context.Transactions.Remove(transaction);
      await context.SaveChangesAsync();

      return new Response<Transaction?>(transaction);

    }
    catch
    {
      return new Response<Transaction?>(null, 500, "Não foi possivel buscar a transação");
    }
  }

  public async Task<Response<Transaction?>> GetByIdAsync(GetTransactionByIdRequest request)
  {
    try
    {
      var transaction = await context
      .Transactions
      .FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);

      return transaction is null
        ? new Response<Transaction?>(null, 404, "Transação não encontrada")
        : new Response<Transaction?>(transaction);
    }
    catch
    {
      return new Response<Transaction?>(null, 500, "Não foi possivel buscar a transação");
    }
  }

  public async Task<PagedResponse<List<Transaction>?>> GetByPeriodAsync(GetTransactionByPeriodRequest request)
  {
    try
    {
      request.StartDate ??= DateTime.Now.GetFirstDay();
      request.EndDate ??= DateTime.Now.GetLastDay();
    }
    catch
    {
      return new PagedResponse<List<Transaction>?>(null, 500, "Não foi possivel determinar a data de início ou término");
    }

    try
    {
      var query = context
      .Transactions
      .AsNoTracking()
      .Where(x => x.CreatedAt >= request.StartDate &&
                  x.CreatedAt <= request.EndDate &&
                  x.UserId == request.UserId)
         .OrderBy(x => x.CreatedAt);


      var transactions = await query
      .Skip((request.PageNumber - 1) * request.PageSize)
      .Take(request.PageSize)
      .ToListAsync();

      var count = await query.CountAsync();

      return new PagedResponse<List<Transaction>?>(transactions, count, request.PageNumber, request.PageSize);
    }
    catch
    {
      return new PagedResponse<List<Transaction>?>(null, 500, "Não foi possivel obter as transações");
    }
  }

}