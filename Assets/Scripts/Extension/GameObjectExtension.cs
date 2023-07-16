using System;
using UnityEngine;
using UnityEngine.Assertions;

namespace Extension
{
    public static class GameObjectExtension
    {
        public static T RequireComponent<T>(this GameObject gameObject)
        {
            Assert.IsTrue(gameObject.TryGetComponent(out T component), $"GameObject {gameObject} required to have {typeof(T)} component. ");
            return component;
        }
        
        public static T RequireComponentInChildren<T>(this GameObject gameObject)
        {
            var component = gameObject.GetComponentInChildren<T>();
            Assert.IsTrue(component != null, $"GameObject {gameObject} required to have {typeof(T)} component in children. ");
            return component;
        }
        
        public static T RequireComponentInParent<T>(this GameObject gameObject)
        {
            var component = gameObject.GetComponentInParent<T>();
            Assert.IsTrue(component != null, $"GameObject {gameObject} required to have {typeof(T)} component in parent. ");
            return component;
        }
        
        public static T RequireComponent<T>(this MonoBehaviour gameObject)
        {
            Assert.IsTrue(gameObject.TryGetComponent(out T component), $"GameObject {gameObject} required to have {typeof(T)} component. ");
            return component;
        }
        
        public static T RequireComponentInChildren<T>(this MonoBehaviour gameObject)
        {
            var component = gameObject.GetComponentInChildren<T>();
            Assert.IsTrue(component != null, $"GameObject {gameObject} required to have {typeof(T)} component in children. ");
            return component;
        }
        
        public static T RequireComponentInParent<T>(this MonoBehaviour gameObject)
        {
            var component = gameObject.GetComponentInParent<T>();
            Assert.IsTrue(component != null, $"GameObject {gameObject} required to have {typeof(T)} component in parent. ");
            return component;
        }
    }
}