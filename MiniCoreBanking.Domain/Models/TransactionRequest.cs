namespace MiniCoreBanking.Domain;

public class TransactionRequest
{
    public string AccountNumber { get; set; }

    public decimal Amount { get; set; }

}