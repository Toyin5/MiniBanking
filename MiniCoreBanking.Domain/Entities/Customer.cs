using System.ComponentModel.DataAnnotations;

namespace MiniCoreBanking.Domain;
public class Customer : BaseEntity
{
    [EmailAddress]
    public required string Email { get; set; }
    public required string Password { get; set; }
    public required string UserName { get; set; }
    public required string Address { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public long IdNumber { get; set; }
    public IDTypes IdType { get; set; }
    public required string PhoneNumber { get; set; }

    public ICollection<Account>? Accounts;
}
