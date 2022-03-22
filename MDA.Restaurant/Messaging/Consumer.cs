
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Messaging
{
    public class Consumer : IDisposable
    {
        private string _queueName;
        private string _hostName;

        private bool _disposed = false;

        public string MyProperty { get; set; }

        public Consumer SetQueueAndHost(string queueName, string hostName)
        {
            _queueName = queueName;
            _hostName = hostName;
            return this;
        }

        public void Receive(EventHandler<BasicDeliverEventArgs> receiveCallBack)
        {
            var factory = new ConnectionFactory() { HostName = _hostName };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            //объявление обменника
            channel.ExchangeDeclare("direct_exchange", "direct");

            //объявление очереди
            channel.QueueDeclare(_queueName, true, false, false, null);

            //привязка
            channel.QueueBind(_queueName, "direct_exchange", _queueName);

            var consumer = new EventingBasicConsumer(channel); //создание consumer для канала

            consumer.Received += receiveCallBack;

            channel.BasicConsume(_queueName, true, consumer);  //стартуем
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
            }
            _disposed = true;
        }

        #endregion
    }
}
