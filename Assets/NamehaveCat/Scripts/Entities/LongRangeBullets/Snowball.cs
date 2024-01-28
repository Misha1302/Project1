namespace NamehaveCat.Scripts.Entities.LongRangeBullets
{
    using NamehaveCat.Scripts.Different;
    using UnityEngine;

    [RequireComponent(typeof(ObjectFlipper))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class Snowball : MonoBehaviour
    {
        private Vector2 _direction;

        private ObjectFlipper _flipper;
        private Rigidbody2D _rb2D;

        private void Awake()
        {
            _flipper = GetComponent<ObjectFlipper>();
            _rb2D = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            _rb2D.velocity = _direction;
        }

        public void Set(LayerMask enemy, Vector3 position, Vector3 direction, bool right)
        {
            _rb2D.excludeLayers = enemy;
            _rb2D.position = position;
            _rb2D.velocity = _direction = direction;
            _flipper.FlipX = right;
        }
    }
}