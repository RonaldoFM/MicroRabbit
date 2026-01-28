using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

var factory = new ConnectionFactory { HostName = "localhost" };

await using var connection = await factory.CreateConnectionAsync();
await using var channel = await connection.CreateChannelAsync();

await channel.QueueDeclareAsync(
    queue: "BasicTest",
    durable: false,
    exclusive: false,
    autoDelete: false,
    arguments: null
);

var consumer = new AsyncEventingBasicConsumer(channel);

consumer.ReceivedAsync += async (model, ea) =>
{
    var body = ea.Body.ToArray();
    var message = Encoding.UTF8.GetString(body);
    Console.WriteLine($" [x] Received {message}");
    await Task.Yield();
};

await channel.BasicConsumeAsync("BasicTest", true, consumer);


Console.WriteLine("Press [Enter] to exit the Consumer app..");
Console.ReadLine();