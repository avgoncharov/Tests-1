using System;
using System.Collections.Generic;
using System.Text;

namespace MessageBroker.Interfaces
{
    public interface IMessage
    {
        string Key { get; set; }
        object Source { get; set; }
    }
}
