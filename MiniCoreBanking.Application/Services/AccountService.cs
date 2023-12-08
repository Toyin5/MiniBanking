using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MiniCoreBanking.Domain;
using MiniCoreBanking.Infrastructure;

namespace MiniCoreBanking.Application;

public class AccountService : IAccountService
{

    private readonly IMapper _mapper;

    private readonly ApplicationDbContext _context;


    private readonly ILogger<AccountService> _logger;

    public AccountService(IMapper mapper, ApplicationDbContext context, ILogger<AccountService> logger)
    {
        _mapper = mapper;
        _context = context;
        _logger = logger;
    }
    public async Task<AccountDto> ActivateAccount(string accountNumber)
    {
        try
        {
            var account = await _context.Accounts.SingleOrDefaultAsync(acc => acc.Number == accountNumber);
            if (account == null)
            {
                throw new Exception("Account not found");
            }
            account.Status = StatusTypes.INACTIVE;
            await _context.SaveChangesAsync();
            return _mapper.Map<AccountDto>(account);
        }
        catch (Exception Ex)
        {
            if (Ex.Message.ToString().Contains("not found"))
            {
                throw new Exception("Account not found");
            }
            _logger.LogError("An error occured while activating an account");
            _logger.LogError(Ex.ToString());
            throw new Exception("Server error!");
        }
    }

    public async Task<AccountDto> CreateAccount(AccountRequest request)
    {
        try
        {
            var CustomerExists = await _context.Customers.FindAsync(request.CustomerID);
            if (CustomerExists == null)
            {
                throw new Exception("Customer not found");
            }
            var accountExists = await _context.Accounts.SingleOrDefaultAsync(acc => acc.CustomerID == request.CustomerID && acc.Type == (AccountTypes)request.AccountType);
            if (accountExists != null)
            {
                throw new Exception("Duplicate Account Type");
            }
            var account = new Account()
            {
                CustomerID = request.CustomerID,
                Type = (AccountTypes)request.AccountType,
                Number = Generate.GenerateAccountNumber()
            };
            _context.Accounts.Add(account);
            await _context.SaveChangesAsync();
            return _mapper.Map<AccountDto>(account);

        }
        catch (Exception Ex)
        {
            if (Ex.Message.ToString().Contains("Duplicate"))
            {
                throw new Exception("Duplicate Account");
            }
            if (Ex.Message.ToString().Contains("Customer"))
            {
                _logger.LogError("Unauthorized account opening");
                throw new Exception("Unauthorized");
            }
            _logger.LogError(Ex.ToString());
            throw new Exception("Server error!");
        }
    }

    public async Task<AccountDto> DeactivateAccount(string accountNumber)
    {
        try
        {
            var account = await _context.Accounts.SingleOrDefaultAsync(acc => acc.Number == accountNumber);
            if (account == null)
            {
                throw new Exception("Account not found");
            }
            account.Status = StatusTypes.INACTIVE;
            await _context.SaveChangesAsync();
            return _mapper.Map<AccountDto>(account);
        }
        catch (Exception Ex)
        {
            if (Ex.Message.ToString().Contains("not found"))
            {
                throw new Exception("Account not found");
            }
            _logger.LogError("An error occured while deactivating an account");
            _logger.LogError(Ex.ToString());
            throw new Exception("Server error!");
        }
    }

    public async Task<AccountDto> DeleteAccount(string accountNumber)
    {
        try
        {
            var account = await _context.Accounts.SingleOrDefaultAsync(acc => acc.Number == accountNumber);
            if (account == null)
            {
                throw new Exception("Account not found");
            }
            _context.Accounts.Remove(account);
            await _context.SaveChangesAsync();
            return _mapper.Map<AccountDto>(account);
        }
        catch (Exception Ex)
        {
            if (Ex.Message.ToString().Contains("not found"))
            {
                throw new Exception("Account not found");
            }
            _logger.LogError("An error occured while deleting an account");
            _logger.LogError(Ex.ToString());
            throw new Exception("Server error!");
        }
    }

    public async Task<AccountDto> Deposit(TransactionRequest request)
    {
        try
        {
            var account = await _context.Accounts.SingleOrDefaultAsync(acc => acc.Number == request.AccountNumber);
            if (account == null)
            {
                throw new Exception("Account not found");
            }
            account.Deposit(request.Amount);
            await _context.SaveChangesAsync();
            return _mapper.Map<AccountDto>(account);
        }
        catch (Exception Ex)
        {
            if (Ex.Message.ToString().Contains("not found"))
            {
                throw new Exception("Account not found");
            }
            _logger.LogError("An error occured while depositing");
            _logger.LogError(Ex.ToString());
            throw new Exception("Server error!");
        }
    }

    public async Task<AccountDto> GetAccount(string accountNumber)
    {
        try
        {
            var account = await _context.Accounts.SingleOrDefaultAsync(acc => acc.Number == accountNumber);
            if (account == null)
            {
                throw new Exception("Account not found");
            }
            return _mapper.Map<AccountDto>(account);
        }
        catch (Exception Ex)
        {
            if (Ex.Message.ToString().Contains("not found"))
            {
                throw new Exception("Account not found");
            }
            _logger.LogError("An error occured while fetching an account");
            _logger.LogError(Ex.ToString());
            throw new Exception("Server error!");
        }
    }

    public async Task<AccountDto> Withdraw(TransactionRequest request)
    {
        try
        {
            var account = await _context.Accounts.SingleOrDefaultAsync(acc => acc.Number == request.AccountNumber);
            if (account == null)
            {
                throw new Exception("Account not found");
            }
            account.Withdraw(request.Amount);
            await _context.SaveChangesAsync();
            return _mapper.Map<AccountDto>(account);
        }
        catch (Exception Ex)
        {
            if (Ex.Message.ToString().Contains("not found"))
            {
                throw new Exception("Account not found");
            }
            _logger.LogError($"An error occured while withdrawing from account");
            _logger.LogError(Ex.ToString());
            throw new Exception("Server error!");
        }
    }
}