

using Bank.Api.Data;
using Bank.Core.Handlers;
using Bank.Core.Models;
using Bank.Core.Requests.Transactions;
using Bank.Core.Responses;

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

    public Task<Response<Transaction?>> DeleteAsync(DeleteTransactionRequest request)
    {
        throw new NotImplementedException();
    }

    public Task<Response<Transaction?>> GetByIdAsync(GetTransactionByIdRequest request)
    {
        throw new NotImplementedException();
    }

    public Task<PagedResponse<List<Transaction>?>> GetByPeriodAsync(GetTransactionByPeriodRequest request)
    {
        throw new NotImplementedException();
    }

    public Task<Response<Transaction?>> UpdateAsync(UpdateTransactionRequest request)
    {
        throw new NotImplementedException();
    }
}