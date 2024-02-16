namespace NamehaveCat.Scripts.Machinery.Checkpoint
{
    using System.Collections.Generic;
    using NamehaveCat.Scripts.Extensions;
    using NamehaveCat.Scripts.Tags;
    using UnityEngine;
    using UnityEngine.Events;

    [RequireComponent(typeof(Collider2D))]
    public class Checkpoint : MonoBehaviour
    {
        private static readonly List<Checkpoint> _checkpoints = new();

        public readonly UnityEvent<Checkpoint> activityChanged = new();
        private bool _wasActivated;

        public bool WasActivated
        {
            get => _wasActivated;
            private set
            {
                if (_wasActivated == value)
                    return;

                _wasActivated = value;
                activityChanged.Invoke(this);
            }
        }

        private void OnEnable()
        {
            _checkpoints.Add(this);
        }

        private void OnDisable()
        {
            _checkpoints.Remove(this);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            _checkpoints.ForEach(x => x.WasActivated = false, this);

            WasActivated = WasActivated || other.TryGetComponent<PlayerTag>(out _);
        }
    }
}