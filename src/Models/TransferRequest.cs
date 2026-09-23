namespace api_poo.Models;

public record TransferRequest(
    string SourceAccountNumber,
    string DestinationAccountNumber,
    decimal Amount);
