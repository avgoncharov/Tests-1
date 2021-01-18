using MessageBroker.Interfaces;
using System;
using Xunit;

namespace MessageBroker.Test
{
    public class UnsubscriberTest
    {
        [Fact]
        public void TestUnsubscriber()
        {
            var messageBroker = new MessageBroker();
            var unsubscriber = new UnsubscriberMock("anotherKey", "unsub", "joe");
            var message = new MessageMock("anotherKey", "test");

            messageBroker.Start();
            messageBroker.Subscribe(unsubscriber);
            messageBroker.Unsubscribe(unsubscriber);
            messageBroker.Post(message);

            Assert.Equal("false", unsubscriber.Status);
            Assert.Null(unsubscriber._message);
        }
    }
    public class UnsubscriberMock : ISubscriber
    {
        public string Key { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }

        public string _message;

        public UnsubscriberMock(string key, string id, string name)
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
