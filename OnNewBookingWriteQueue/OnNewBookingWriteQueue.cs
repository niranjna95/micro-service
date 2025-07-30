using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace OnNewBookingWriteQueue;

public class OnNewBookingWriteQueue
{
    private readonly ILogger<OnNewBookingWriteQueue> _logger;

    public OnNewBookingWriteQueue(ILogger<OnNewBookingWriteQueue> logger)
    {
        _logger = logger;
    }

    [Function("OnNewBookingWriteQueue")]
    public IActionResult Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequest req)
    {
        _logger.LogInformation("C# HTTP trigger function processed a request.");
        return new OkObjectResult("Welcome to Azure Functions!");
    }
}