using Microsoft.AspNetCore.Mvc;
using api_poo.Interfaces;
using System.Buffers;
using api_poo.Data;
using api_poo.Entities;
using api_poo.Models;

namespace api_poo.Controllers;


[ApiController]

[Route("[controller]")]
public class TransferController : ControllerBase
{
	private readonly IBankAccountRepository _bankAccountRepository;

	public TransferController(IBankAccountRepository bankAccountRepository)
	{
		_bankAccountRepository = bankAccountRepository;
	}

	[HttpPost]
	public ActionResult Transfer([FromBody] TransferRequest request)
	{
		if (request.Amount <= 0)
		{
			return BadRequest("Transfer amount must be positive.");
		}

		if (request.SourceAccountNumber == request.DestinationAccountNumber)
		{
			return BadRequest("Source and destination accounts must be different.");
		}

		BankAccount sourceAccount;
		BankAccount destinationAccount;

		try
		{
			sourceAccount = _bankAccountRepository.GetByNumber(request.SourceAccountNumber);
			destinationAccount = _bankAccountRepository.GetByNumber(request.DestinationAccountNumber);
		}
		catch (KeyNotFoundException exception)
		{
			return NotFound(exception.Message);
		}

		try
		{
			sourceAccount.MakeWithdrawal(request.Amount, DateTime.UtcNow, "Transfer");
			destinationAccount.MakeDeposit(request.Amount, DateTime.UtcNow, "Transfer");
		}
		catch (InvalidOperationException exception)
		{
			return BadRequest(exception.Message);
		}

		return NoContent();
	}
}
