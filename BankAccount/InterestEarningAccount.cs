namespace BankAccount;

internal class InterestEarningAccount(string name, decimal initialBalance)
    : BankAccount(name, initialBalance)
{
    public override void PerformMonthEndTransactions()
    {
        if (Balance > 500m)
        {
            decimal interest = Balance * 0.02m;
            MakeDeposit(interest, DateTime.Now, "apply monthly interest");
        }
    }
}
