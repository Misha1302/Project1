using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Head : MonoBehaviour
{
    [SerializeField] private Enemy enemy;
    [SerializeField] private float stunTime;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<PlayerTag>(out _))
            enemy.Stun(stunTime);
    }
}