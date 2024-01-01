using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    [SerializeField] private Image healthBar;

    private void Start()
    {
        GameManager.Instance.PlayerHealth.onDamage.AddListener(h => healthBar.fillAmount = (float)(h.Value / 100f));
    }
}