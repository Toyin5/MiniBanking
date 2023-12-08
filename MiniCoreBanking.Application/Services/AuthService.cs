using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MiniCoreBanking.Domain;
using MiniCoreBanking.Infrastructure;

namespace MiniCoreBanking.Application;
public class AuthService : IAuthService
{
    private readonly IJwtService _jwtService;
    private readonly ILogger<AuthService> _logger;

    private readonly ApplicationDbContext _context;

    private readonly IMapper _mapper;

    public AuthService(IJwtService jwtService, ILogger<AuthService> logger,
        ApplicationDbContext context, IMapper mapper)
    {
        _jwtService = jwtService;
        _logger = logger;
        _context = context;
        _mapper = mapper;
    }

    public async Task<CustomerDto> RegisterAsync(CustomerRequest request)
    {
        try
        {
            var CustomerExists = await _context.Customers.FirstOrDefaultAsync(customer => customer.Email == request.Email && customer.UserName == request.UserName);
            if (CustomerExists != null)
            {
                _logger.Log(LogLevel.Information, "duplicate credentials found");
                throw new Exception("Duplicate credentials found");
            }
            var customer = new Customer()
            {
                Email = request.Email,
                Password = SecretHasher.Hash(request.Password),
                UserName = request.UserName,
                FirstName = request.FirstName,
                LastName = request.LastName,
                IdNumber = request.IdNumber,
                IdType = (IDTypes)request.IdType,
                PhoneNumber = request.PhoneNumber,
                Address = request.Address
            };
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();
            _logger.Log(LogLevel.Information, "A user is created");
            return _mapper.Map<CustomerDto>(customer);

        }
        catch (Exception Ex)
        {
            _logger.LogError(Ex.ToString());
            throw new Exception(Ex.ToString());
        }
    }

    public async Task<TokenResponse> LoginAsync(LoginRequest request)
    {
        // try
        // {
        var user = await _context.Customers.FirstOrDefaultAsync(customer => customer.Email == request.Email);

        if (user == null)
        {
            _logger.Log(LogLevel.Warning, "Attempteed login with invalid email address");
            throw new Exception("Invalid email/password");
        }

        var result = SecretHasher.Verify(request.Password, user.Password);
        if (result == false)
        {
            _logger.LogError($"There is an attempt to login user: {request.Email} with wrong password");
            throw new Exception("Wrong password!");
        }

        var token = _jwtService.CreateToken(user);
        return token;

        // }
        // catch (Exception Ex)
        // {
        //     _logger.LogError(Ex.ToString());
        //     throw new Exception("Server Error: " + Ex.ToString());
        // }
    }
}