namespace BankAccount;


internal class GiftCardAccount(string name, decimal initialBalance, decimal monthlyDeposit = 0)
    : BankAccount(name, initialBalance)
{
    private readonly decimal _monthlyDeposit = monthlyDeposit;

    public override void PerformMonthEndTransactions()
    {
        if (_monthlyDeposit != 0)
        {
            MakeDeposit(_monthlyDeposit, DateTime.Now, "Add monthly deposit");
        }
    }
}
