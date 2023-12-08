namespace MiniCoreBanking.Domain;
public record CustomerDto(
    string Email,
    string Address,
    string UserName,
    string FirstName,
    string LastName,
    long IdNumber,
    int IdType,
    string PhoneNumber,
    string Id,
    StatusTypes Status
);