namespace NamehaveCat.Scripts.Different
{
    using UnityEngine;

    public class Follower : MonoBehaviour
    {
        [SerializeField] private Transform follow;
        [SerializeField] private Vector3 offset;

        private void LateUpdate()
        {
            transform.position = follow.position + offset;
        }
    }
}