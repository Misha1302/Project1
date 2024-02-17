namespace NamehaveCat.Scripts.Helpers
{
    using System;
    using System.Collections;
    using JetBrains.Annotations;
    using NamehaveCat.Scripts.MImplementations;
    using NamehaveCat.Scripts.Tags;
    using UnityEngine;

    public class CoroutineManager : MonoBehaviour
    {
        private EmptyMonoBeh _alwaysEnabled;
        private CoroutineRepository _coroutineRepository;

        private void Awake()
        {
            gameObject.AddComponent<DontPauseTag>();

            _alwaysEnabled =
                GameObjectsCreator.New<DontPauseTag, EmptyMonoBeh>("AlwaysEnabledCoroutineKeeper");

            _coroutineRepository = new CoroutineRepository();
        }

        public void StartCoroutine(IEnumerator coroutine, [CanBeNull] string coroutineName = null)
        {
            _alwaysEnabled.StartCoroutine(
                AddStartStopRemoveCoroutine(coroutine, coroutineName ?? GameData.DefaultCoroutineName));
        }

        public void StopCoroutines(string groupName)
        {
            if (!_coroutineRepository.TryGetCoroutines(groupName, out var list))
                return;

            list.ForEach(_alwaysEnabled.StopCoroutine);
            _coroutineRepository.RemoveAllCoroutines(groupName);
        }

        public void InvokeAfter(Action action, float timeToWait, [CanBeNull] string coroutineName = null)
        {
            StartCoroutine(InvokeAfterCoroutine(action, timeToWait), coroutineName);
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


        private sealed class EmptyMonoBeh : MonoBehaviour
        {
        }
    }
}