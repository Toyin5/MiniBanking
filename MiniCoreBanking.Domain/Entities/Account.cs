namespace MiniCoreBanking.Domain;
public class Account : BaseEntity
{
    public required string Number { get; set; }
    public required string CustomerID { get; set; }
    public decimal Balance { get; set; }


    public AccountTypes Type { get; set; }

    public Currencies Currency { get; set; }
}