using MiniCoreBanking.Domain;

namespace MiniCoreBanking.Application;
public interface ICustomerService
{
    public Task<CustomerDto> UpdateCustomer(UpdateCustomerRequest request, string id);
    public Task<CustomerDto> ActivateCustomer(string id);
    public Task<CustomerDto> DeactivateCustomer(string id);
    // public Task<CustomerDto>[] GetCustomers();
    public Task<CustomerDto> DeleteCustomer(string id);

}