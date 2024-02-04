namespace NamehaveCat.Scripts
{
    using System.Collections;
    using System.Collections.Generic;
    using NamehaveCat.Scripts.Different;
    using NamehaveCat.Scripts.Tags;
    using UnityEngine;

    public class CoroutineManager : MonoBehSingleton<CoroutineManager>, IDontPauseTag
    {
        private readonly Dictionary<string, List<IEnumerator>> _dict = new();

        private IEnumerator _coroutinesToWait;

        private MonoBehaviourInstance _alwaysEnabled;

        private void Start()
        {
            _alwaysEnabled = new GameObject()
                .AddComponent<DontPauseTag>().gameObject
                .AddComponent<MonoBehaviourInstance>();
        }

        public void StartCoroutine(IEnumerator coroutine, string coroutineName = "Nameless")
        {
            if (!_dict.ContainsKey(coroutineName))
                _dict.Add(coroutineName, new List<IEnumerator>());

            (_dict[coroutineName] ??= new List<IEnumerator>()).Add(coroutine);

            _alwaysEnabled.StartCoroutine(coroutine);
        }

        public void StopCoroutines(string coroutineName)
        {
            if (!_dict.TryGetValue(coroutineName, out var list))
                return;

            list.ForEach(StopCoroutine);
            _dict[coroutineName] = new List<IEnumerator>();
        }
    }
}