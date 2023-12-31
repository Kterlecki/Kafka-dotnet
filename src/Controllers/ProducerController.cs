using System.Diagnostics;
using System.Net;
using System.Text.Json;
using Confluent.Kafka;
using kafka_dotnet.Models;
using Microsoft.AspNetCore.Mvc;
namespace kafka_dotnet.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProducerController : ControllerBase
{
    private readonly string bootstrapServer = "localhost:9092";
    private readonly string topic = "quickstart-events";

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] OrderRequest orderRequest)
    {
        string message = JsonSerializer.Serialize(orderRequest);
        return Ok(await SendOrderRequest(topic, message));
    }
    private async Task<bool> SendOrderRequest(string topic, string message)
    {
        ProducerConfig config  = new ProducerConfig{
            BootstrapServers = bootstrapServer,
            ClientId = Dns.GetHostName()
        };

        try
        {
            using(var producer = new ProducerBuilder<Null, string>(config).Build()){
                var result = await producer.ProduceAsync(topic, new Message<Null, string> {
                    Value = message
                });
            Debug.WriteLine($"Delivery Timestamp: {result.Timestamp.UtcDateTime}");
            return await Task.FromResult(true);
            }
        } catch (Exception ex)
        {
            Console.WriteLine($"Error occured: {ex.Message}");
        }
        return await Task.FromResult(false);
    }
}