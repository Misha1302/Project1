namespace NamehaveCat.Scripts.Different
{
    using System;
    using System.Linq;
    using NamehaveCat.Scripts.Helpers;
    using UnityEngine;

    [DefaultExecutionOrder(-1)]
    public class MonoBehSingleton<T> : MonoBehaviour where T : MonoBehSingleton<T>
    {
        public static T Instance { get; private set; }

        protected virtual void Awake()
        {
            if (Instance != null)
                ThrowManyInstances();

            Instance = (T)this;
        }

        protected virtual void OnDestroy()
        {
            Instance = null;
        }

        private static void ThrowManyInstances()
        {
            Thrower.Throw(
                new InvalidOperationException(
                    $"There are many singletons! ({string.Join(", ", FindObjectsOfType<MonoBehSingleton<T>>().Select(x => x.name))})"
                )
            );
        }
    }
}