using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

[ApiController]
[Route("[controller]")]
public class PaymentController : ControllerBase
{
    private readonly ILogger<PaymentController> logger;

    public PaymentController(ILogger<PaymentController> logger)
    {
        this.logger = logger;
    }

    [HttpPost]
    public bool Post()
    {
        return true;
    }
}
