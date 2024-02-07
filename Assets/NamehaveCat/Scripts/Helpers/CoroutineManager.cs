namespace NamehaveCat.Scripts.Helpers
{
    using System;
    using System.Collections;
    using NamehaveCat.Scripts.MImplementations;
    using NamehaveCat.Scripts.Tags;
    using UnityEngine;

    public class CoroutineManager : MonoBehaviour, IDontPauseTag
    {
        private MonoBehaviourInstance _alwaysEnabled;
        private CoroutineRepository _coroutineRepository;

        private void Start()
        {
            _alwaysEnabled = new GameObject()
                .AddComponent<DontPauseTag>().gameObject
                .AddComponent<MonoBehaviourInstance>();

            _coroutineRepository = new CoroutineRepository();
        }

        public void StartCoroutine(IEnumerator coroutine, string coroutineName = "Nameless")
        {
            _alwaysEnabled.StartCoroutine(AddStartStopRemoveCoroutine(coroutine, coroutineName));
        }

        public void StopCoroutines(string groupName)
        {
            if (!_coroutineRepository.TryGetCoroutines(groupName, out var list))
                return;

            list.ForEach(_alwaysEnabled.StopCoroutine);
            _coroutineRepository.RemoveAllCoroutines(groupName);
        }

        public void InvokeAfter(Action action, float timeToWait)
        {
            StartCoroutine(InvokeAfterCoroutine(action, timeToWait));
        }

        private static IEnumerator InvokeAfterCoroutine(Action action, float timeToWait)
        {
            yield return new MWaitForSeconds(timeToWait);
            action();
        }

        private IEnumerator AddStartStopRemoveCoroutine(IEnumerator coroutine, string coroutineName)
        {
            _coroutineRepository.AddCoroutine(coroutine, coroutineName);

            yield return coroutine;

            _coroutineRepository.RemoveCoroutine(coroutine, coroutineName);
        }
    }
}