using Microsoft.AspNetCore.Identity;
using MiniCoreBanking.Domain;

namespace MiniCoreBanking.Application;

public interface IJwtService
{
    public TokenResponse CreateToken(Customer user);


}