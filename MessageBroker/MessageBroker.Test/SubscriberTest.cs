using MessageBroker.Interfaces;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace MessageBroker.Test
{
    public class SubscriberTest
    {
        [Fact]
        public void TestSubscriberAndPost()
        {
            var messageBroker = new MessageBroker();
            var subscriber = new SubscriberMock("key", "sub", "andy");
            var message = new MessageMock("key", "source");

            messageBroker.Start();
            messageBroker.Subscribe(subscriber);
            messageBroker.Post(message);

            Assert.Equal("true", subscriber.Status);
            var cooldown = Task.Run(() => Thread.Sleep(3000));
            Assert.Equal("source", subscriber._message);
        }
    }
    public class SubscriberMock : ISubscriber
    {
        public string Key { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }

        public string _message;

        public SubscriberMock(string key, string id, string name)
        {
            Key = key;
            Id = id;
            Name = name;
        }

        public void InsertMessage(object message)
        {
            _message = message.ToString();
        }
    }
}
