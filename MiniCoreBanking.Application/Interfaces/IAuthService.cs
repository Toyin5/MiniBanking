using MiniCoreBanking.Domain;
namespace MiniCoreBanking.Application;
public interface IAuthService
{
    public Task<CustomerDto> RegisterAsync(CustomerRequest request);
    public Task<TokenResponse> LoginAsync(LoginRequest request);
}