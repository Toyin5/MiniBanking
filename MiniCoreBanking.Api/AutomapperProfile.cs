using AutoMapper;
using MiniCoreBanking.Domain;
namespace mini_banking;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Customer, CustomerDto>();
        CreateMap<Account, AccountDto>();
    }
}