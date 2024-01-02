using UnityEngine;

public class Follower : MonoBehaviour
{
    [SerializeField] private Transform follow;
    [SerializeField] private Vector3 offset;

    private void FixedUpdate()
    {
        transform.position = follow.position + offset;
    }
}