namespace NamehaveCat.Scripts.Entities.Player
{
    using System;
    using System.Collections;
    using NamehaveCat.Scripts.Different;
    using NamehaveCat.Scripts.Entities.Enemy;
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
            if (health.Value > 0) return;

            panel.gameObject.SetActive(true); // enable death panel
            messageText.text = string.Format(format, health.Message); // set death text
            GameManager.Instance.PlayerController.Rb2D.constraints =
                RigidbodyConstraints2D.FreezeAll; // stopping player

            // disable all player components
            KillPlayer(GameManager.Instance.PlayerController.transform.GetComponent<Animator>());

            // load next scene after X seconds
            CoroutineManager.Instance.StartCoroutine(
                InvokeAfter(Death.Die, AnimatorHelper.DeathAnimationsTotalTime)
            );
        }

        private static IEnumerator InvokeAfter(Action action, float deathAnimationsTotalTime)
        {
            yield return new MWaitForSeconds(deathAnimationsTotalTime);
            action();
        }

        private void KillPlayer(Animator player)
        {
            IsDying = true;
            player.SetBool(AnimatorHelper.Death, true);

            GameManager.Instance.PlayerController.enabled = false;
            GameManager.Instance.PlayerHealth.enabled = false;
        }
    }
}