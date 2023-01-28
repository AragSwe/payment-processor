using Microsoft.AspNetCore.Mvc;

namespace account_transactions.Controllers;

[ApiController]
[Route("[controller]")]
public class TransactionsController : ControllerBase
{
    private readonly ILogger<TransactionsController> _logger;

    public TransactionsController(ILogger<TransactionsController> logger)
    {
        _logger = logger;
    }
}
