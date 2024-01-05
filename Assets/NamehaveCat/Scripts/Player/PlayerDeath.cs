namespace NamehaveCat.Scripts.Player
{
    using System;
    using System.Collections;
    using NamehaveCat.Scripts.Different;
    using TMPro;
    using UnityEngine;
    using UnityEngine.UI;

    public class PlayerDeath : MonoBehaviour
    {
        [SerializeField] private Image panel;
        [SerializeField] private TMP_Text messageText;
        [SerializeField] private string format = "{0}";

        private void Start()
        {
            GameManager.Instance.PlayerHealth.onDamage.AddListener(health =>
            {
                if (health.Value > 0)
                    return;

                panel.gameObject.SetActive(true); // enable death panel
                messageText.text = string.Format(format, health.Message); // set death text
                GameManager.Instance.PlayerController.Rb2D.constraints = RigidbodyConstraints2D.FreezeAll; // stopping player

                // disable all player components
                KillPlayer(
                    GameManager.Instance.PlayerController.transform.GetComponent<Animator>()
                );

                // load next scene after X seconds
                StartCoroutine(InvokeAfter(Death.Die, AnimatorHelper.DeathAnimationsTotalTime));
            });
        }

        private static IEnumerator InvokeAfter(Action action, float deathAnimationsTotalTime)
        {
            yield return new WaitForSeconds(deathAnimationsTotalTime);
            action();
        }

        private static void KillPlayer(Animator player)
        {
            player.SetBool(AnimatorHelper.Death, true);

            GameManager.Instance.PlayerController.enabled = false;
            GameManager.Instance.PlayerHealth.enabled = false;
        }
    }
}