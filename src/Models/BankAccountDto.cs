using api_poo.Entities;

namespace api_poo.Models;

// DTO significa Data Transfer Object (objeto de transferencia de datos).
// Se usa para decidir qué información sale de la API. En este caso exponemos
// solamente Number, Owner y Balance, y no enviamos toda la entidad ni sus transacciones.
//
// 'record' genera una clase orientada a transportar datos y recibe sus valores
// mediante este constructor primario: string Number, string Owner y decimal Balance.
public record BankAccountDto(string Number, string Owner, decimal Balance)
{
    // Método fábrica: transforma una entidad de dominio en el DTO que devuelve
    // el controlador al cliente HTTP.
    public static BankAccountDto Create(BankAccount entity)
    {
        var dto = new BankAccountDto(
                    entity.Number,
                    entity.Owner,
                    entity.Balance
                );

        return dto;
    }

    public static List<BankAccountDto> Create(IEnumerable<BankAccount> entities)
    {
        var listDto = new List<BankAccountDto>();
        foreach (var entity in entities)
        {
            listDto.Add(Create(entity));
        }
        return listDto;
    }

    // Fin del record BankAccountDto.
}