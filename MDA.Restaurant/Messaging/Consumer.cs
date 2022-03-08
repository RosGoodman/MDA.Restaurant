
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Messaging
{
    public class Consumer : IDisposable
    {
        private readonly string _queueName;
        private readonly IConnection _connection;
        private readonly IModel _channel;

        private bool _disposed = false;

        public Consumer(string queueName, string hostName)
        {
            _queueName = queueName;
            var factory = new ConnectionFactory() { HostName = hostName };

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
        }

        public void Receive(EventHandler<BasicDeliverEventArgs> reseiveCallback)
        {
            //объявление обменника
            _channel.ExchangeDeclare("direct_exchange", "direct");

            //объявление очереди
            _channel.QueueDeclare(_queueName, false, false, false, null);

            //привязка
            _channel.QueueBind(_queueName, "direct_exchange", _queueName);

            var consumer = new EventingBasicConsumer(_channel); //создание consumer для канала
            consumer.Received += reseiveCallback;   //добавление обработчика события приема сообщения

            _channel.BasicConsume(_queueName, true, consumer);  //стартуем
        }

        #region dispose

        ~Consumer()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return;
            if (disposing)
            {
                _connection.Dispose();
                _channel.Dispose();
            }
            _disposed = true;
        }

        #endregion
    }
}
