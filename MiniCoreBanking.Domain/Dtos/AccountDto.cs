namespace MiniCoreBanking.Domain;

public record AccountDto(
    string Number,
    string Id,
    string CustomerID,
    decimal Balance,

    StatusTypes Status,


    AccountTypes Type,

    Currencies Currency
);