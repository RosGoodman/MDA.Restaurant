
using Messaging;
using Microsoft.Extensions.Hosting;
using System.Diagnostics;
using System.Text;

namespace Notification;

public class Worker : BackgroundService
{
    private readonly Consumer _consumer;

    public Worker()
    {
        //важно чтобы имя очереди совпадало
        _consumer = new Consumer().SetQueueAndHost("BookingNotification", "localhost");
        ExecuteAsync();
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken = default)
    {
        _consumer.Receive((sender, args) =>
        {
            var body = args.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);    //декодирование
            Console.WriteLine(" [x] Received {0}", message);
            Debug.Print(message);
        });
    }
}
