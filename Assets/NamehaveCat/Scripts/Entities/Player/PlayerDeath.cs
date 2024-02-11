namespace NamehaveCat.Scripts.Entities.Player
{
    using System.Collections;
    using NamehaveCat.Scripts.Different;
    using NamehaveCat.Scripts.Health;
    using NamehaveCat.Scripts.Helpers;
    using TMPro;
    using UnityEngine;

    public class PlayerDeath : MonoBehaviour
    {
        [SerializeField] private DeathPanel deathPanel;
        [SerializeField] private float animationDuration = 2f;
        [SerializeField] private float waitBeforeDie = 3.5f;
        [SerializeField] private TMP_Text messageText;
        [SerializeField] private string format = "{0}...";

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

            if (health.DamageInfo.DamageType == DamageType.Default)
                GameManager.Instance.CoroutineManager.StartCoroutine(ShadowScreen());

            DisablePlayer(health.DamageInfo.DamageType);

            Die();
        }

        private void Die()
        {
            GameManager.Instance.CoroutineManager.InvokeAfter(Death.Die, waitBeforeDie);
        }

        private void UiUpdate(Health health)
        {
            deathPanel.BlockUI();
            messageText.text = string.Format(format, health.DamageInfo.Message);
        }

        private IEnumerator ShadowScreen()
        {
            var waitForFixedUpdate = new WaitForFixedUpdate();

            while (deathPanel.Alpha < 0.99f)
            {
                deathPanel.Alpha += 1 / animationDuration * Time.fixedDeltaTime;
                yield return waitForFixedUpdate;
            }
        }

        private void DisablePlayer(DamageType damageType)
        {
            IsDying = true;

            var playerAnimator = GameManager.Instance.PlayerController.transform.GetComponent<Animator>();

            if (damageType == DamageType.Default)
            {
                playerAnimator.SetBool(AnimatorHelper.Death, true);

                GameManager.Instance.PlayerController.Rb2D.constraints = RigidbodyConstraints2D.FreezeAll;
            }
            else
            {
                playerAnimator.enabled = false;
            }

            GameManager.Instance.PlayerController.enabled = false;
            GameManager.Instance.PlayerHealth.enabled = false;
        }
    }
}