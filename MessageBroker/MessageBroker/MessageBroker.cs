using MessageBroker.Interfaces;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MessageBroker
{
  public class MessageBroker : IDisposable
  {
    private ConcurrentQueue<IMessage> _messages = new ConcurrentQueue<IMessage>();
    private Dictionary<string, List<ISubscriber>> _subscribersMsg = new Dictionary<string, List<ISubscriber>>();
    private CancellationTokenSource _tokenSource = new CancellationTokenSource();

    public Task Start()
    {
      return Task.Run(() => Loop(), _tokenSource.Token);
    }

    public void Subscribe(ISubscriber subscriber)
    {
      if (!_subscribersMsg.ContainsKey(subscriber.Key))
      {
        _subscribersMsg[subscriber.Key] = new List<ISubscriber>();
      }

      _subscribersMsg[subscriber.Key].Add(subscriber);

      subscriber.Status = "true";
    }

    public void Unsubscribe(ISubscriber subscriber)
    {
      _subscribersMsg[subscriber.Key].Remove(subscriber);

      subscriber.Status = "false";
    }

    public void Post(IMessage message)
    {
      _messages.Enqueue(message);
    }

    public void Dispose()
    {
      _tokenSource.Cancel();
    }
    
    private void Loop()
    {
      while (true)
      {
        if (_messages.TryDequeue(out var msg) && _subscribersMsg.TryGetValue(msg.Key, out var subs))
        {
          foreach (var sub in subs)
          {
            sub.InsertMessage(msg.Source);
          }
        }
      }
    }
  }
}
