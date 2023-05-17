using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMSProject.WPF.Messenger
{
    public abstract class Message
    {
        public object Sender { get; private set; }
        public Message(object sender)
        {
            Sender = sender;
        }
    }
}
