using System.Reflection.Metadata;
using api_poo.Interfaces;
using api_poo.Entities;

namespace api_poo.Data;

public class BankAccountRepositoryEF : IBankAccountRepository
{
    private static List<BankAccount> _accounts = [];

    public BankAccount GetByNumber(string accountNumber)
    {
        throw new Exception("Not implemented");
    }

    public List<BankAccount> List()
    {
        throw new Exception("Not implemented");
    }

    public BankAccount Add(BankAccount entity)
    {
        _accounts.Add(entity);
        return entity;
    }

    public void Update(BankAccount entity)
    {
        throw new Exception("Not implemented");
    }

    public void Delete(BankAccount entity)
    {
        throw new Exception("Not implemented");
    }

}