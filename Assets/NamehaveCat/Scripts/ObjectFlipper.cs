namespace NamehaveCat.Scripts
{
    using UnityEngine;

    public class ObjectFlipper : MonoBehaviour
    {
        [SerializeField] private Transform[] othersToFlip;
        private bool _flipX;

        private readonly Quaternion _left = Quaternion.Euler(0, 0, 0);
        private readonly Quaternion _right = Quaternion.Euler(0, 180, 0);

        public bool FlipX
        {
            get => _flipX;
            set
            {
                _flipX = value;

                transform.rotation = FlipX ? _right : _left;
                foreach (var other in othersToFlip) 
                    other.rotation = FlipX ? _right : _left;
            }
        }
    }
}