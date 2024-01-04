using UnityEngine;

namespace NamehaveCat.Scripts
{
    public class ObjectFlipper : MonoBehaviour
    {
        private bool _flipX;

        public bool FlipX
        {
            get => _flipX;
            set
            {
                _flipX = value;

                transform.rotation = Quaternion.Euler(0, FlipX ? 180 : 0, 0);
            }
        }
    }
}