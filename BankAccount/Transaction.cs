namespace BankAccount;

public class Transaction(decimal amount, DateTime date, string note)
{
    public decimal Amount { get; } = amount;
    public DateTime Date { get; } = date;
    public string Notes { get; } = note;

}