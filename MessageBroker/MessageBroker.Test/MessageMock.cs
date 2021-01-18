using MessageBroker.Interfaces;

namespace MessageBroker.Test
{
    public class MessageMock : IMessage
    {
        public string Key { get; set; }
        public object Source { get; set; }

        public MessageMock(string key, string source)
        {
            Key = key;
            Source = source;
        }
    }
}
