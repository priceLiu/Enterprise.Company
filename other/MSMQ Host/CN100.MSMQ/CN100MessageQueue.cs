using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Messaging;

namespace CN100.MSMQ
{
    public class CN100MessageQueue : System.Messaging.MessageQueue
    {
        public CN100MessageQueue()
            : base()
        { }
        public CN100MessageQueue(string path)
            : base(path)
        { }
        public CN100MessageQueue(string path, bool sharedModeDenyReceive)
            : base(path, sharedModeDenyReceive)
        { }
        public CN100MessageQueue(string path, QueueAccessMode accessMode)
            : base(path, accessMode)
        {
        }
        public CN100MessageQueue(string path, bool sharedModeDenyReceive, bool enableCache)
            : base(path, sharedModeDenyReceive, enableCache)
        {
        }
        public CN100MessageQueue(string path, bool sharedModeDenyReceive, bool enableCache, QueueAccessMode accessMode)
            : base(path, sharedModeDenyReceive, enableCache, accessMode)
        { }

        public ItemConfig QueueConfig { get; set; }
    }
}
