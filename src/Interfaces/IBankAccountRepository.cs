using api_poo.Entities;

namespace api_poo.Interfaces;

// Una interfaz define un contrato: indica qué operaciones debe ofrecer un
// repositorio, pero no explica cómo se implementa cada operación.
public interface IBankAccountRepository
{
    // Busca una cuenta usando su número.
    BankAccount GetByNumber(string accountNumber);

    // Devuelve todas las cuentas disponibles.
    List<BankAccount> List();

    // Agrega una cuenta y devuelve la entidad agregada.
    BankAccount Add(BankAccount entity);

    // Actualiza una cuenta existente.
    void Update(BankAccount entity);

    // Elimina una cuenta existente.
    void Delete(BankAccount entity);
}

// Fin de la interfaz IBankAccountRepository.