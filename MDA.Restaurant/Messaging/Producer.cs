
using RabbitMQ.Client;
using System.Text;

namespace Messaging
{
    public class Producer : IProducer
    {
        private string _queueName;
        private IConnection _connection;
        private IModel _channel;

        private bool _disposed = false;

        public IProducer SetQueueAndHost(string queueName, string hostName)
        {
            _queueName = queueName;
            var factory = new ConnectionFactory() { HostName = hostName };

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            return this;
        }

        public void Send(string message)
        {
            //объявление обменника
            _channel.ExchangeDeclare("direct_exchange", "direct", false, false, null);

            var body = Encoding.UTF8.GetBytes(message); //формирование тела сообщения для отправки

            _channel.BasicPublish("direct_exchange", _queueName, null, body);   //отправка сообщения
        }

        #region dispose

        ~Producer()
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
