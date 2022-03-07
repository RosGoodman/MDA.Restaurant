
using RabbitMQ.Client;
using System;
using System.Text;

namespace Messaging
{
    public class Produser : IDisposable
    {
        private readonly string _queueName;
        private readonly IConnection _connection;
        private readonly IModel _channel;

        private bool _disposed = false;

        public Produser(string queueName, string hostName)
        {
            _queueName = queueName;
            var factory = new ConnectionFactory() { HostName = hostName };

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
        }

        public void Send(string message)
        {
            //объявление обменника
            _channel.ExchangeDeclare("direct_exchange", "direct", false, false, null);

            var body = Encoding.UTF8.GetBytes(message); //формирование тела сообщения для отправки

            _channel.BasicPublish("direct_exchange", _queueName, null, body);   //отправка сообщения
        }

        #region dispose

        ~Produser()
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
