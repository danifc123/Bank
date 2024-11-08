namespace Bank.Core.Requests.Transactions;

public class GetTransactionByPeriodRequest : PagedRequest
{
  public DateTime? StartDate { get; set; }
  public DateTime? EndDate { get; set; }
}