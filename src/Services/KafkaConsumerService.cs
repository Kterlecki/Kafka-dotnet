using System.Diagnostics;
using System.Text.Json;
using Confluent.Kafka;
using kafka_dotnet.Models;

namespace kafka_dotnet.Services;

public class KafkaConsumerService : IHostedService
{
    private readonly string _topic = "quickstart-events";
    private readonly string _groupId = "test_group";
    private readonly string bootstrapServers = "localhost:9092";
    public Task StartAsync(CancellationToken cancellationToken)
    {
        var config = new ConsumerConfig
        {
            GroupId = _groupId,
            BootstrapServers = bootstrapServers,
            AutoOffsetReset = AutoOffsetReset.Earliest
        };
        try{
            using(var consumerBuilder = new ConsumerBuilder<Ignore, string>(config).Build()){
                consumerBuilder.Subscribe(_topic);
                var cancelToken = new CancellationTokenSource();

                try
                {
                    while (true)
                    {
                        var consumer = consumerBuilder.Consume(cancelToken.Token);
                        var orderRequest = JsonSerializer.Deserialize<OrderProcessingRequest>(consumer.Message.Value);
                        Debug.WriteLine($"Processing Order Id: {orderRequest.OrderId}");
                    }
                }
                catch (OperationCanceledException)
                {
                    consumerBuilder.Close();
                }
            }
        }
        catch(Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
