namespace account_transactions.Accounts;

public class AccountsRepository
{
    private readonly ILogger<AccountsRepository> _logger;

    public AccountsRepository(ILogger<AccountsRepository> logger)
    {
        _logger = logger;
    }
}