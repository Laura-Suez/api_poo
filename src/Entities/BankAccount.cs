using System.ComponentModel.DataAnnotations.Schema;
using api_poo.Entities;

namespace api_poo.Entities;

public class BankAccount
{
    // Surrogate primary key. EF needs this.
    public int Id { get; private set; }

    // Was get-only; EF needs to be able to set it. private set is fine.
    public string Number { get; private set; } = string.Empty;

    // Renamed nothing here, but now the domain ctor param is "owner" to match.
    public string Owner { get; private set; } = string.Empty;

    // Real navigation. EF maps this as a one-to-many with Transaction.
    public List<Transaction> Transactions { get; private set; } = new();

    // Computed from the navigation, not stored as a column.
    [NotMapped]
    public decimal Balance => Transactions.Sum(t => t.Amount);

    private static int s_accountNumberSeed = 1234567890;

    // EF Core uses this to materialize rows.
    private BankAccount() { }

    // Your domain constructor, kept for application code.
    public BankAccount(string owner, decimal initialBalance)
    {
        Owner = owner;
        Number = (s_accountNumberSeed++).ToString();
        MakeDeposit(initialBalance, DateTime.Now, "Initial balance");
    }

    public void MakeDeposit(decimal amount, DateTime date, string note)
    {
        if (amount <= 0)
            throw new ArgumentOutOfRangeException(nameof(amount), "Amount of deposit must be positive");

        Transactions.Add(new Transaction(amount, date, note));
    }

    public void MakeWithdrawal(decimal amount, DateTime date, string note)
    {
        if (amount <= 0)
            throw new ArgumentOutOfRangeException(nameof(amount), "Amount of withdrawal must be positive");

        if (Balance - amount < 0)
            throw new InvalidOperationException("Not sufficient funds for this withdrawal");

        Transactions.Add(new Transaction(-amount, date, note));
    }

    public virtual void PerformMonthEndTransactions() { }

    public string GetAccountHistory()
    {
        var report = new System.Text.StringBuilder();
        decimal balance = 0;

        report.AppendLine("Date\t\tAmount\tBalance\tNote");
        foreach (var item in Transactions)
        {
            balance += item.Amount;
            report.AppendLine($"{item.Date.ToShortDateString()}\t{item.Amount}\t{balance}\t{item.Notes}");
        }

        return report.ToString();
    }
}