using UnityEngine;

public class FatalDamage : MonoBehaviour
{
    [SerializeField] private string message;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.TryGetComponent<Health>(out var health))
            health.Damage(float.MaxValue, message);
    }
}