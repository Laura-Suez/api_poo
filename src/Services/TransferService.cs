using api_poo.Entities;
using api_poo.Interfaces;
using api_poo.Models;

namespace api_poo.Services;

public class TransferService
{
    private readonly IBankAccountRepository _bankAccountRepository;

    public TransferService(IBankAccountRepository bankAccountRepository)
    {
        _bankAccountRepository = bankAccountRepository;
    }

    public void Transfer(TransferRequest request)
    {
        if (request.Amount <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(request.Amount), "Transfer amount must be positive.");
        }

        if (request.SourceAccountNumber == request.DestinationAccountNumber)
        {
            throw new InvalidOperationException("Source and destination accounts must be different.");
        }

        BankAccount sourceAccount = _bankAccountRepository.GetByNumber(request.SourceAccountNumber);
        BankAccount destinationAccount = _bankAccountRepository.GetByNumber(request.DestinationAccountNumber);

        sourceAccount.MakeWithdrawal(request.Amount, DateTime.UtcNow, "Transfer");
        destinationAccount.MakeDeposit(request.Amount, DateTime.UtcNow, "Transfer");
    }
}
