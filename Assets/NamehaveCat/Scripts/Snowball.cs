namespace NamehaveCat.Scripts
{
    using JetBrains.Annotations;
    using NamehaveCat.Scripts.Different;
    using UnityEngine;

    [RequireComponent(typeof(ObjectFlipper))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class Snowball : MonoBehaviour
    {
        [CanBeNull] private ObjectFlipper _flipper;
        [CanBeNull] private Rigidbody2D _rb2D;

        public ObjectFlipper Flipper => _flipper ??= GetComponent<ObjectFlipper>();
        public Rigidbody2D Rb2D => _rb2D ??= GetComponent<Rigidbody2D>();
    }
}