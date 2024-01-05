namespace NamehaveCat.Scripts.Different
{
    using NamehaveCat.Scripts.Velocipedi;
    using UnityEngine;
    using UnityEngine.UI;

    public class UiManager : MonoBehaviour
    {
        [SerializeField] private Image healthBar;

        [SerializeField] private RButton btnLeft;
        [SerializeField] private RButton btnRight;
        [SerializeField] private RButton btnUp;

        public RButton BtnLeft => btnLeft;
        public RButton BtnRight => btnRight;
        public RButton BtnUp => btnUp;

        private void Start()
        {
            GameManager.Instance.PlayerHealth.onDamage.AddListener(h => healthBar.fillAmount = h.Value / 100f);
        }
    }
}