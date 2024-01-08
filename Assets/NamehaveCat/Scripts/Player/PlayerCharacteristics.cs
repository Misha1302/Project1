namespace NamehaveCat.Scripts.Player
{
    using UnityEngine;

    public class PlayerCharacteristics : MonoBehaviour
    {
        [SerializeField] private float playerSize = 1.35f;

        public float PlayerSize => playerSize;
    }
}