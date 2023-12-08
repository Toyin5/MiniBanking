using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MiniCoreBanking.Application;
using MiniCoreBanking.Domain;

namespace MiniCoreBanking.Api;
[Route("api/auth")]
[ApiController]
public class AuthController : ControllerBase
{

    private readonly ILogger<AuthController> _logger;

    private readonly IAuthService _service;

    public AuthController(ILogger<AuthController> logger, IAuthService service)
    {
        this._logger = logger;
        this._service = service;
    }

    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> Register([FromBody] CustomerRequest userDetails)
    {
        if (!ModelState.IsValid || userDetails == null)
        {
            return new BadRequestObjectResult(new { Message = "User Registration Failed" });
        }

        try
        {
            Console.WriteLine(userDetails.UserName + "line 37");
            var result = await _service.RegisterAsync(userDetails);

            return Ok(new { Message = "User Registration Successful", data = result });
        }
        catch (Exception Ex)
        {
            if (Ex.Message.ToString().Contains("Duplicate credentials found"))
            {
                return new BadRequestObjectResult(new { error = "Duplicate credentials! Email or Username has been used" });
            }
            if (Ex.Message.ToString().Contains("Registration failed!"))
            {
                return new BadRequestObjectResult(new { error = "Registration failed" });
            }
            _logger.LogError(Ex.Message.ToString());
            return BadRequest(new { error = "Server error! Registration failed!", err = Ex.Message.ToString() });
        }

    }

    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest credentials)
    {
        if (!ModelState.IsValid || credentials == null)
        {
            return new BadRequestObjectResult(new { Message = "Login failed" });
        }

        try
        {
            var result = await _service.LoginAsync(credentials);
            if (result == null)
            {
                return BadRequest(new { error = "Error getting token" });
            }
            return Ok(new { Message = "You are logged in", data = result });

        }
        catch (Exception Ex)
        {
            if (Ex.Message == "Invalid")
            {
                return NotFound(new { error = "Invalid credentials", err = Ex.Message.ToString() });
            }
            if (Ex.Message == "Wrong password")
            {
                return Unauthorized(new { error = "Wrong password", err = Ex.Message.ToString() });
            }
            _logger.LogError(Ex.Message.ToString());
            return BadRequest(new { error = "Server error! Login failed!", err = Ex.Message.ToString() });
        }

    }


}