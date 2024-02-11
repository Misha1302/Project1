namespace NamehaveCat.Scripts
{
    using System.Collections;
    using System.Globalization;
    using NamehaveCat.Scripts.Different;
    using TMPro;
    using UnityEngine;

    [RequireComponent(typeof(TMP_Text))]
    public class FpsVisualizer : MonoBehaviour
    {
        private readonly FpsCollection _fpsCollection = new();
        private TMP_Text _text;

        private void Start()
        {
            _text = GetComponent<TMP_Text>();

            GameManager.Instance.CoroutineManager.StartCoroutine(PrintAverageFps());
        }

        private void Update()
        {
            _fpsCollection.Add(1f / Time.deltaTime);
        }

        private IEnumerator PrintAverageFps()
        {
            while (true)
            {
                _text.text = _fpsCollection.Average().ToString(CultureInfo.InvariantCulture);

                yield return new WaitForSeconds(0.2f);
            }
            // ReSharper disable once IteratorNeverReturns
        }

        private sealed class FpsCollection
        {
            private int _index;
            private readonly float[] _fps = new float[60];

            public void Add(float fps)
            {
                _fps[_index] = fps;
                _index = ++_index % _fps.Length;
            }

            public float Average()
            {
                var sum = 0f;
                // ReSharper disable once LoopCanBeConvertedToQuery
                foreach (var item in _fps)
                    sum += item;

                return sum / _fps.Length;
            }
        }
    }
}