namespace NamehaveCat.Scripts
{
    using System.Collections.Generic;
    using NamehaveCat.Scripts.Tags;
    using UnityEngine;

    [RequireComponent(typeof(Collider2D))]
    public class Checkpoint : MonoBehaviour
    {
        private static readonly List<Checkpoint> _checkpoints = new();

        public bool WasActivated { get; private set; }

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
            _checkpoints.ForEach(x => x.WasActivated = false);
            WasActivated = WasActivated || other.TryGetComponent<PlayerTag>(out _);
        }
    }
}