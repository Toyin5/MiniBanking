namespace MiniCoreBanking.Domain;
public static class AccountExtensions
{
    public static void Deposit(this Account account, decimal amount)
    {
        if (amount <= 0)
        {
            throw new Exception("Invalid amount");
        }
        account.Balance += amount;
    }
    public static void Withdraw(this Account account, decimal amount)
    {
        if (amount > account.Balance)
        {
            throw new Exception("Invalid amount! You can't draw more than your balance");
        }
        if (amount <= 0)
        {
            throw new Exception("Invalid amount");
        }
        account.Balance -= amount;
    }
}