namespace MiniCoreBanking.Domain;

public class AccountRequest
{
    public string CustomerID { get; set; }
    public int AccountType { get; set; }

    public int AccountCurrency { get; set; }
}