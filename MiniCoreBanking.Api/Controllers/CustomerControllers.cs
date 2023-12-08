namespace MiniCoreBanking.Api;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using MiniCoreBanking.Application;
using MiniCoreBanking.Domain;

[ApiController]
[Route("/api")]
public class CustomerControllers : ControllerBase
{
  private readonly ILogger<CustomerControllers> _logger;
  private readonly ICustomerService _service;

  public CustomerControllers(ILogger<CustomerControllers> logger, ICustomerService service)
  {
    _logger = logger;
    _service = service;
  }


  // [Authorize]
  [HttpPut("customer/{id}")]
  public async Task<IActionResult> UpdateCustomerAsync(UpdateCustomerRequest request, string id)
  {
    try
    {
      var res = await _service.UpdateCustomer(request, id);
      return Ok(new { message = "Success", res });
    }
    catch (Exception Ex)
    {
      if (Ex.Message.Contains("Customer not found"))
      {
        return new NotFoundObjectResult(new { error = "Invalid id! Customer with id not found" });
      }
      return new BadRequestObjectResult(new { error = "Server Error", err = Ex.Message.ToString() });
    }
  }

  [HttpDelete("customer/{id}")]
  public async Task<IActionResult> DeleteCustomer(string id)
  {
    try
    {
      var res = await _service.DeleteCustomer(id);
      return Ok(new { message = "Success", res });

    }
    catch (Exception Ex)
    {
      if (Ex.Message.Contains("Customer not found"))
      {
        return new NotFoundObjectResult(new { error = "Invalid id! Customer with id not found" });
      }
      return new BadRequestObjectResult(new { error = "Server Error", err = Ex.Message.ToString() });
    }
  }

  [HttpPatch("activate/customer/{id}")]
  public async Task<IActionResult> ActivateCustomer(string id)
  {
    try
    {
      var res = await _service.ActivateCustomer(id);
      return Ok(new { message = "Success", res });

    }
    catch (Exception Ex)
    {
      if (Ex.Message.Contains("Customer not found"))
      {
        return new NotFoundObjectResult(new { error = "Invalid id! Customer with id not found" });
      }
      return new BadRequestObjectResult(new { error = "Server Error", err = Ex.Message.ToString() });
    }
  }

  [HttpPatch("deactivate/customer/{id}")]
  public async Task<IActionResult> DeactivateCustomer(string id)
  {
    try
    {
      var res = await _service.DeactivateCustomer(id);
      return Ok(new { message = "Success", res });
    }
    catch (Exception Ex)
    {
      if (Ex.Message.Contains("Customer not found"))
      {
        return new NotFoundObjectResult(new { error = "Invalid id! Customer with id not found" });
      }
      return new BadRequestObjectResult(new { error = "Server Error", err = Ex.Message.ToString() });
    }
  }



}