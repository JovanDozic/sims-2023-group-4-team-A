using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMSProject.WPF.Messenger
{
    public static class MessageBus
    {
        private static Dictionary<Type, List<Action<Message>>> subscribers =
        new Dictionary<Type, List<Action<Message>>>();

        public static void Subscribe<TMessage>(object subscriber, Action<TMessage> handler) where TMessage : Message
        {
            Type messageType = typeof(TMessage);
            if (!subscribers.ContainsKey(messageType))
            {
                subscribers.Add(messageType, new List<Action<Message>>());
            }

            subscribers[messageType].Add((message) =>
            {
                handler((TMessage)message);
            });
        }

        public static void Unsubscribe<TMessage>(object subscriber) where TMessage : Message
        {
            Type messageType = typeof(TMessage);
            if (subscribers.ContainsKey(messageType))
            {
                subscribers[messageType].RemoveAll((action) =>
                {
                    return action.Target == subscriber;
                });
            }
        }

        public static void Publish<TMessage>(TMessage message) where TMessage : Message
        {
            Type messageType = typeof(TMessage);
            if (subscribers.ContainsKey(messageType))
            {
                foreach (Action<Message> handler in subscribers[messageType])
                {
                    handler(message);
                }
            }
        }
    }
}

