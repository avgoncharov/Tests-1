using System;
using System.Collections.Generic;
using System.Text;

namespace MessageBroker.Interfaces
{
    public interface ISubscriber
    {
        string Key { get; set; }
        string Id { get; set; }
        string Name { get; set; }
        string Status { get; set; }

        void InsertMessage(object message);
    }
}
