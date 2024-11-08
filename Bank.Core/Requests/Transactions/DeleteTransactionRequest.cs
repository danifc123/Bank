namespace Bank.Core.Requests.Transactions;

public class DeleteTransactionRequest : Request
{
  public long Id { get; set; }
}