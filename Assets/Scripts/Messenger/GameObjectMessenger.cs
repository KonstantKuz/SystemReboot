using System;
using System.Collections.Generic;
using Common;
using SuperMaxim.Core.Extensions;
using UnityEngine;

namespace Messenger
{
    public class GameObjectMessenger
    {
        private readonly GameObject _gameObject;
        private readonly Dictionary<Type, object> _publishers = new Dictionary<Type, object>();
        
        public GameObjectMessenger(GameObject gameObject)
        {
            _gameObject = gameObject;
        }

        public void Publish<T>(T message)
        {
            if (!_publishers.ContainsKey(typeof(T)))
            {
                _publishers[typeof(T)] = new Publisher<T>(_gameObject);
            }

            var pub = _publishers[typeof(T)] as Publisher<T>;
            pub.Publish(message);
        }
        
        private class Publisher<TMessage>
        {
            private readonly HashSet<IMessageListener<TMessage>> _listeners;

            public Publisher(GameObject gameObject)
            {
                var listeners = gameObject.GetComponentsInChildren<IMessageListener<TMessage>>();
                _listeners = new HashSet<IMessageListener<TMessage>>(listeners);
            }
            
            public void Publish(TMessage message)
            {
                _listeners.ForEach(it => it.OnMessage(message));
            }
        }
    }
}