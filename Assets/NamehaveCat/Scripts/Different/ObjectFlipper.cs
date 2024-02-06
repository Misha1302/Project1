namespace NamehaveCat.Scripts.Different
{
    using UnityEngine;

    public class ObjectFlipper : MonoBehaviour
    {
        private static readonly Quaternion _left = Quaternion.Euler(0, 0, 0);
        private static readonly Quaternion _right = Quaternion.Euler(0, 180, 0);

        [SerializeField] private Transform[] othersToFlip;
        private bool _flipX;

        public bool FlipX
        {
            set
            {
                _flipX = value;

                transform.rotation = _flipX ? _right : _left;
                foreach (var other in othersToFlip)
                    other.rotation = _flipX ? _right : _left;
            }
        }
    }
}