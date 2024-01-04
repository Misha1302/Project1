using System;
using System.Linq;
using UnityEngine;

namespace NamehaveCat.Scripts
{
    public class MonoBehSingleton<T> : MonoBehaviour where T : MonoBehSingleton<T>
    {
        public static T Instance { get; private set; }

        private void Awake()
        {
            if (Instance is not null)
                ThrowManyInstances();

            Instance = (T)this;
        }

        private void OnDestroy()
        {
            Instance = null;
        }

        private static void ThrowManyInstances()
        {
            throw new InvalidOperationException(
                $"There are many singletons! ({String.Join(", ", FindObjectsOfType<MonoBehSingleton<T>>().Select(x => x.name))})"
            );
        }
    }
}