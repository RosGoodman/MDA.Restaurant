
using RabbitMQ.Client;
using System.Text;

namespace Messaging
{
    public class Producer : IProducer
    {
        private string _queueName;
        private string _hostName;

        private bool _disposed = false;

        public IProducer SetQueueAndHost(string queueName, string hostName)
        {
            _queueName = queueName;
            _hostName = hostName;
            return this;
        }

        public void Send(string message)
        {
            var factory = new ConnectionFactory() { HostName = _hostName };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();            
            
            //объявление обменника
            channel.ExchangeDeclare("direct_exchange", "direct", false, false, null);

            var body = Encoding.UTF8.GetBytes(message); //формирование тела сообщения для отправки

            channel.BasicPublish("", _queueName, null, body);   //отправка сообщения
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
            }
            _disposed = true;
        }

        #endregion
    }
}
