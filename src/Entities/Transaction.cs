namespace api_poo.Entities;

public record Transaction(decimal Amount, DateTime Date, string Notes)
{
    public int Id { get; init; }
    public int BankAccountId { get; init; }
}