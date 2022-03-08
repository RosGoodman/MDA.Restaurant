using System;

namespace Messaging
{
    public interface IProducer : IDisposable
    {
        void Dispose();
        void Send(string message);
        IProducer SetQueueAndHost(string queueName, string hostName);
    }
}