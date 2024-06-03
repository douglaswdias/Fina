namespace Fina.Core.Requests.Transactions;

public class GetTransactionByDateRequest : PagedRequest
{
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
}
