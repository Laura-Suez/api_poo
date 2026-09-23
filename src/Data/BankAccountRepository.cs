using api_poo.Interfaces;
using api_poo.Entities;

namespace api_poo.Data;

// Esta clase se encarga de guardar y recuperar cuentas bancarias.
// En este ejemplo funciona como un repositorio en memoria: los datos se
// almacenan en una lista y se pierden cuando la aplicación se detiene.
//
// Los dos puntos indican que la clase implementa la interfaz
// IBankAccountRepository. Por eso debe definir todos los métodos de la interfaz.
public class BankAccountRepository : IBankAccountRepository
{
    // static hace que exista una única lista compartida por todas las instancias
    // del repositorio creadas por la aplicación.
    private readonly AppDbContext _context;

    public BankAccountRepository(AppDbContext context)
    {
        _context = context;
    }

    // Busca una cuenta por su número.
    // Si no existe una cuenta, se lanza una excepción en lugar de devolver null,
    // porque el contrato de la interfaz declara un BankAccount no nullable.
    public BankAccount GetByNumber(string accountNumber)
    {
        return _accounts.FirstOrDefault(account => account.Number == accountNumber)
            ?? throw new KeyNotFoundException($"Bank account {accountNumber} not found.");
    }

    // Devuelve una copia de la lista. ToList evita entregar directamente la lista
    // interna y permite que el repositorio conserve el control de sus datos.
    public List<BankAccount> List()
    {
        return _context.BankAccounts.ToList();
    }

    // Agrega una nueva entidad a la colección y devuelve la misma cuenta agregada.
    public BankAccount Add(BankAccount entity)
    {
        _context.BankAccounts.Add(entity);
        return entity;
    }

    // Reemplaza la cuenta existente que tenga el mismo número.
    public void Update(BankAccount entity)
    {
        _context.BankAccounts.Update(entity);
    }

    // Elimina todas las cuentas cuyo número coincida con el de la entidad recibida.
    public void Delete(BankAccount entity)
    {
        _context.BankAccounts.Remove(entity);
    }

// Fin de la clase BankAccountRepository.
}