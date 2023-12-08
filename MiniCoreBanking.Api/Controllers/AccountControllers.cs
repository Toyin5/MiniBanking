namespace MiniCoreBanking.Api;
using Microsoft.AspNetCore.Mvc;
using MiniCoreBanking.Application;
using MiniCoreBanking.Domain;

[ApiController]
[Route("/api")]
public class AccountControllers : ControllerBase
{
    private readonly ILogger<AccountControllers> _logger;

    private readonly IAccountService _service;

    public AccountControllers(ILogger<AccountControllers> logger, IAccountService service)
    {
        _logger = logger;
        _service = service;
    }

    [HttpPost("account")]
    public async Task<IActionResult> CreateAccount(AccountRequest request)
    {
        try
        {
            var res = await _service.CreateAccount(request);
            return Ok(new { message = "Success", res });

        }
        catch (Exception Ex)
        {
            if (Ex.Message.ToString().Contains("Duplicate"))
            {
                return new BadRequestObjectResult(new { error = "Duplicate Account" });
            }
            if (Ex.Message.ToString().Contains("Unauthorized"))
            {
                return new UnauthorizedObjectResult(new { error = "Invalid Customer ID" });
            }
            if (Ex.Message.ToString().Contains("Server"))
            {
                return new BadRequestObjectResult(new { error = "Server Error" });
            }
            return new BadRequestObjectResult(new { error = "Server Error" });
        }
    }

    [HttpGet("account/{accountNumber}")]
    public async Task<IActionResult> GetAccount(string accountNumber)
    {
        try
        {
            var res = await _service.GetAccount(accountNumber);
            return Ok(new { message = "Success", res });
        }
        catch (Exception Ex)
        {
            if (Ex.Message.ToString().Contains("Account not found"))
            {
                return new NotFoundObjectResult(new { error = "Account not found" });
            }
            if (Ex.Message.ToString().Contains("Server"))
            {
                return new BadRequestObjectResult(new { error = "Server Error" });
            }
            return new BadRequestObjectResult(new { error = "Server Error" });
        }
    }
    [HttpPatch("activate/account/{id}")]
    public async Task<IActionResult> ActivateAccount(string accountNumber)
    {
        try
        {
            var res = await _service.ActivateAccount(accountNumber);
            return Ok(new { message = "Success", res });
        }

        catch (Exception Ex)
        {
            if (Ex.Message.ToString().Contains("Not found"))
            {
                return new NotFoundObjectResult(new { error = "Account not found" });
            }
            if (Ex.Message.ToString().Contains("Server"))
            {
                return new BadRequestObjectResult(new { error = "Server Error" });
            }
            return new BadRequestObjectResult(new { error = "Server Error" });
        }

    }
    [HttpPatch("deactivate/account/{id}")]
    public async Task<IActionResult> DeactivateAccount(string accountNumber)
    {
        try
        {
            var res = await _service.DeactivateAccount(accountNumber);
            return Ok(new { message = "Success", res });
        }

        catch (Exception Ex)
        {
            if (Ex.Message.ToString().Contains("Not found"))
            {
                return new NotFoundObjectResult(new { error = "Account not found" });
            }
            if (Ex.Message.ToString().Contains("Server"))
            {
                return new BadRequestObjectResult(new { error = "Server Error" });
            }
            return new BadRequestObjectResult(new { error = "Server Error" });
        }
    }
    [HttpDelete("account/{id}")]
    public async Task<IActionResult> DeleteAccount(string accountNumber)
    {
        try
        {
            var res = await _service.DeleteAccount(accountNumber);
            return Ok(new { message = "Success", res });
        }

        catch (Exception Ex)
        {
            if (Ex.Message.ToString().Contains("Not found"))
            {
                return new NotFoundObjectResult(new { error = "Account not found" });
            }
            if (Ex.Message.ToString().Contains("Server"))
            {
                return new BadRequestObjectResult(new { error = "Server Error" });
            }
            return new BadRequestObjectResult(new { error = "Server Error" });
        }
    }

    [HttpPost("account/deposit")]
    public async Task<IActionResult> Deposit(TransactionRequest request)
    {
        try
        {
            var res = await _service.Deposit(request);
            return Ok(new { message = "Success", res });
        }

        catch (Exception Ex)
        {
            if (Ex.Message.ToString().Contains("Not found"))
            {
                return new NotFoundObjectResult(new { error = "Account not found" });
            }
            if (Ex.Message.ToString().Contains("Server"))
            {
                return new BadRequestObjectResult(new { error = "Server Error" });
            }
            return new BadRequestObjectResult(new { error = "Server Error" });
        }
    }

    [HttpPost("account/withdraw")]
    public async Task<IActionResult> Withdraw(TransactionRequest request)
    {
        try
        {
            var res = await _service.Withdraw(request);
            return Ok(new { message = "Success", res });
        }

        catch (Exception Ex)
        {
            if (Ex.Message.ToString().Contains("Not found"))
            {
                return new NotFoundObjectResult(new { error = "Account not found" });
            }
            if (Ex.Message.ToString().Contains("Server"))
            {
                return new BadRequestObjectResult(new { error = "Server Error" });
            }
            return new BadRequestObjectResult(new { error = "Server Error" });
        }
    }
}