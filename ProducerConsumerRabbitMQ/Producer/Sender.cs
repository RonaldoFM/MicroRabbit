using RabbitMQ.Client;
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

string message = "Getting started with .Net Core RabbitMQ!";
var body = Encoding.UTF8.GetBytes(message);

await channel.BasicPublishAsync(
    exchange: "",
    routingKey: "BasicTest",
    body: body
);

Console.WriteLine($" [x] Sent {message}");

Console.WriteLine("Press [Enter] to exit the sender app..");
Console.ReadLine();