namespace NamehaveCat.Scripts.Entities.Player
{
    using Cinemachine;
    using NamehaveCat.Scripts.Different;
    using NamehaveCat.Scripts.Health;
    using NamehaveCat.Scripts.Helpers;
    using TMPro;
    using UnityEngine;
    using UnityEngine.UI;

    public class PlayerDeath : MonoBehaviour
    {
        [SerializeField] private Image panel;
        [SerializeField] private TMP_Text messageText;
        [SerializeField] private string format = "{0}";
        [SerializeField] private CinemachineVirtualCamera cinemachineFollower;

        public bool IsDying { get; private set; }

        private void Start()
        {
            GameManager.Instance.PlayerHealth.onDamage.AddListener(OnDamageHandler);
        }

        private void OnValidate()
        {
            if (cinemachineFollower == null)
                cinemachineFollower = FindObjectOfType<CinemachineVirtualCamera>();
        }

        private void OnDamageHandler(Health health)
        {
            if (health.Value > 0)
                return;

            UiUpdate(health);

            DisablePlayer(health.DamageInfo.DamageType);

            Die();
        }

        private static void Die()
        {
            GameManager.Instance.CoroutineManager.InvokeAfter(Death.Die, AnimatorHelper.DeathAnimationsTotalTime);
        }

        private void UiUpdate(Health health)
        {
            panel.gameObject.SetActive(true);
            messageText.text = string.Format(format, health.DamageInfo.Message);
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

                var body = (CinemachineFramingTransposer)cinemachineFollower.GetCinemachineComponent(
                    CinemachineCore.Stage.Body
                );

                body.m_UnlimitedSoftZone = true;
                body.m_XDamping = 4;
                body.m_YDamping = 4;
                body.m_ZDamping = 4;
            }

            GameManager.Instance.PlayerController.enabled = false;
            GameManager.Instance.PlayerHealth.enabled = false;
        }
    }
}