namespace NamehaveCat.Scripts.Entities.Player
{
    using NamehaveCat.Scripts.Different;
    using TMPro;
    using UnityEngine;
    using UnityEngine.UI;

    public class PlayerDeath : MonoBehaviour
    {
        [SerializeField] private Image panel;
        [SerializeField] private TMP_Text messageText;
        [SerializeField] private string format = "{0}";

        public bool IsDying { get; private set; }

        private void Start()
        {
            GameManager.Instance.PlayerHealth.onDamage.AddListener(OnDamageHandler);
        }

        private void OnDamageHandler(Health health)
        {
            if (health.Value > 0)
                return;

            UiUpdate(health);

            DisablePlayer(GameManager.Instance.PlayerController.transform.GetComponent<Animator>());

            Die();
        }

        private static void Die()
        {
            GameManager.Instance.CoroutineManager.InvokeAfter(Death.Die, AnimatorHelper.DeathAnimationsTotalTime);
        }

        private void UiUpdate(Health health)
        {
            panel.gameObject.SetActive(true);
            messageText.text = string.Format(format, health.Message);
        }

        private void DisablePlayer(Animator player)
        {
            IsDying = true;
            player.SetBool(AnimatorHelper.Death, true);

            GameManager.Instance.PlayerController.Rb2D.constraints = RigidbodyConstraints2D.FreezeAll;
            GameManager.Instance.PlayerController.enabled = false;
            GameManager.Instance.PlayerHealth.enabled = false;
        }
    }
}