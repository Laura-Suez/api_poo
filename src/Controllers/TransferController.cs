using Microsoft.AspNetCore.Mvc;
using api_poo.Interfaces;
using System.Buffers;
using api_poo.Data;
using api_poo.Entities;
using api_poo.Models;
using api_poo.Services;

namespace api_poo.Controllers;


[ApiController]

[Route("[controller]")]
public class TransferController : ControllerBase
{
	private readonly TransferService _transferService;

	public TransferController(TransferService transferService)
	{
		_transferService = transferService;
	}

	[HttpPost]
	public ActionResult Transfer([FromBody] TransferRequest request)
	{
		try
		{
			_transferService.Transfer(request);
		}
		catch (KeyNotFoundException exception)
		{
			return NotFound(exception.Message);
		}
		catch (ArgumentOutOfRangeException exception)
		{
			return BadRequest(exception.Message);
		}
		catch (InvalidOperationException exception)
		{
			return BadRequest(exception.Message);
		}

		return NoContent();
	}
}
