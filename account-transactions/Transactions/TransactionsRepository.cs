namespace account_transactions.Transactions;
public class TransactionsRepository
{
    private readonly ILogger<TransactionsRepository> _logger;

    public TransactionsRepository(ILogger<TransactionsRepository> logger)
    {
        _logger = logger;
    }
}