namespace NamehaveCat.Scripts.Health
{
    using UnityEngine;
    using UnityEngine.UI;

    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private Image healthBar;

        private void Start()
        {
            GameManager.Instance.PlayerHealth.onDamage.AddListener(
                h => healthBar.fillAmount = h.Value / 100f
            );
        }
    }
}