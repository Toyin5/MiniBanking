using MiniCoreBanking.Domain;
using AutoMapper;
using MiniCoreBanking.Infrastructure;
using Microsoft.Extensions.Logging;

namespace MiniCoreBanking.Application;

public class CustomerService : ICustomerService
{
    private readonly IMapper _mapper;

    private readonly ApplicationDbContext _context;

    private readonly ILogger<CustomerService> _logger;

    public CustomerService(IMapper mapper, ApplicationDbContext context, ILogger<CustomerService> logger)
    {
        _mapper = mapper;
       
        _context = context;
        _logger = logger;
    }


    public async Task<CustomerDto> UpdateCustomer(UpdateCustomerRequest request, string id)
    {
        try
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
            {
                throw new Exception("Customer not found");
            }
            customer.Update(request);
            await _context.SaveChangesAsync();
            return _mapper.Map<CustomerDto>(customer);
        }
        catch (Exception Ex)
        {
            _logger.LogError("An error occured while updating customer");
            _logger.LogError(Ex.ToString());
            throw new Exception("Server error!");
        }

    }
    public async Task<CustomerDto> DeleteCustomer(string id)
    {
        try
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
            {
                throw new Exception("Customer not found");
            }
            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();
            return _mapper.Map<CustomerDto>(customer);

        }
        catch (Exception Ex)
        {
            _logger.LogError("An error occured while deleting customer");
            _logger.LogError(Ex.ToString());
            throw new Exception("Server error!");
        }
    }

    public async Task<CustomerDto> ActivateCustomer(string id)
    {
        try
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
            {
                throw new Exception("Customer not found");
            }
            customer.Status = StatusTypes.ACTIVE;
            await _context.SaveChangesAsync();
            return _mapper.Map<CustomerDto>(customer);

        }
        catch (Exception Ex)
        {
            _logger.LogError("An error occured while activating customer");
            _logger.LogError(Ex.ToString());
            throw new Exception("Server error!");
        }
    }
    public async Task<CustomerDto> DeactivateCustomer(string id)
    {
        try
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
            {
                throw new Exception("Customer not found");
            }
            customer.Status = StatusTypes.INACTIVE;
            await _context.SaveChangesAsync();
            return _mapper.Map<CustomerDto>(customer);
        }
        catch (Exception Ex)
        {
            _logger.LogError("An error occured while activating customer");
            _logger.LogError(Ex.ToString());
            throw new Exception("Server error!");
        }
    }

    // public Task<CustomerDto[]> GetCustomers()
    // {
    //     var customer = await _context.Customers.;
    //     return _mapper.Map<CustomerDto[]>(allCustomers);
    // }

}