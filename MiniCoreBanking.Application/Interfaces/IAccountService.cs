using MiniCoreBanking.Domain;

namespace MiniCoreBanking.Application;

public interface IAccountService
{
    public Task<AccountDto> CreateAccount(AccountRequest request);

    public Task<AccountDto> GetAccount(string id);

    public Task<AccountDto> Deposit(TransactionRequest request);

    public Task<AccountDto> Withdraw(TransactionRequest request);

    public Task<AccountDto> DeleteAccount(string id);

    public Task<AccountDto> ActivateAccount(string id);
    public Task<AccountDto> DeactivateAccount(string id);


}