namespace NamehaveCat.Scripts.Debug
{
    using System;
    using NamehaveCat.Scripts.Extensions;
    using UnityEngine;

    public class MaxMinYDebugViewer : MonoBehaviour
    {
#if UNITY_EDITOR
        [SerializeField] private Color color;

        private float _maxY = float.MinValue;
        private float _minY = float.MaxValue;

        private void Update()
        {
            var pos = transform.position;

            _minY = Math.Min(_minY, pos.y);
            _maxY = Math.Max(_maxY, pos.y);
        }

        private void OnDrawGizmos()
        {
            var pos = transform.position;

            Gizmos.color = color;
            Gizmos.DrawLine(pos.WithY(_minY) - new Vector3(5, 0), pos.WithY(_minY) + new Vector3(5, 0));
            Gizmos.DrawLine(pos.WithY(_maxY) - new Vector3(5, 0), pos.WithY(_maxY) + new Vector3(5, 0));

            // print((_maxY - _minY).ToString("0.00"));
        }
#endif
    }
}