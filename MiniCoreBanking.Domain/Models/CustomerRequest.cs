namespace MiniCoreBanking.Domain;
public class CustomerRequest
{
    public string Email { get; set; }
    public string Password { get; set; }
    public string UserName { get; set; }
    public string Address { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public long IdNumber { get; set; }
    public int IdType { get; set; }
    public string PhoneNumber { get; set; }
}