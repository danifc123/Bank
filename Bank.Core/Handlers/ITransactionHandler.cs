using Bank.Core.Models;
using Bank.Core.Requests.Transactions;
using Bank.Core.Responses;

namespace Bank.Core.Handlers;

public interface ITransactionHandler
{
   Task<Response<Transaction?>> CreateAsync(CreateTransactionRequest request);
   Task<Response<Transaction?>> UpdateAsync(UpdateTransactionRequest request);
   Task<Response<Transaction?>> DeleteAsync(DeleteTransactionRequest request);
   Task<Response<Transaction?>> GetByIdAsync(GetTransactionByIdRequest request);
   Task<PagedResponse<List<Transaction>?>> GetByPeriodAsync(GetTransactionByPeriodRequest request);
}