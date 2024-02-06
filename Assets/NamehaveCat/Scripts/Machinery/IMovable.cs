namespace NamehaveCat.Scripts.Machinery
{
    using UnityEngine;
    using UnityEngine.Events;

    public interface IMovable
    {
        public UnityEvent<Vector2> OnMove { get; }
    }
}