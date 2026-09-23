using Microsoft.AspNetCore.Mvc;
using api_poo.Interfaces;
using System.Buffers;
using api_poo.Data;
using api_poo.Entities;
using api_poo.Models;

namespace api_poo.Controllers;


[ApiController]

[Route("[controller]")]
public class AccountController : ControllerBase
{
    // Dependencia que el controlador necesita para trabajar con cuentas.
    // Se declara como interfaz para que el controlador no dependa de una
    // implementación concreta del almacenamiento.
    private IBankAccountRepository _bankAccountRepository;

    // Constructor de la clase. ASP.NET Core obtiene automáticamente una instancia
    // de IBankAccountRepository desde el contenedor de dependencias y la entrega
    // como argumento. Este mecanismo se llama inyección por constructor.
    public AccountController(IBankAccountRepository bankAccountRepository)
    {
        _bankAccountRepository = bankAccountRepository;
    }

    // [HttpPost] conecta este método con las peticiones POST /Account.
    // El resultado se declara como ActionResult<BankAccountDto> porque puede
    // representar una respuesta HTTP que contiene un BankAccountDto.
    [HttpPost]
    public ActionResult<BankAccountDto> Post([FromBody] PostAccountRequest prPostAccountRequest)
    {

        // Se crea la entidad de dominio usando los datos recibidos en la petición.
        BankAccount new_bankAccount = new(prPostAccountRequest.Owner,prPostAccountRequest.InitialBalance);

        // El controlador utiliza la abstracción del repositorio para guardar la cuenta.
        _bankAccountRepository.Add(new_bankAccount);
        // Se transforma la entidad a DTO antes de enviarla al cliente.
        return BankAccountDto.Create(new_bankAccount);
        
    }

    [HttpGet]
     public ActionResult<List<BankAccountDto>> Get()
    {

        //List<BankAccount> result = _bankAccountRepository.List(); 
        var result =  _bankAccountRepository.List();
        
        return BankAccountDto.Create(result);
        
    }

    [HttpGet("{accountNumber}")]
     public ActionResult<BankAccountDto> GetByNumber([FromRoute] string accountNumber)
    {
        var result =  _bankAccountRepository.GetByNumber(accountNumber);
        
        return BankAccountDto.Create(result);
        
    }


    // Fin de la clase AccountController.
}
