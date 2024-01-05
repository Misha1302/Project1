﻿using UnityEngine;
using UnityEngine.UI;

namespace NamehaveCat.Scripts
{
    public class UiManager : MonoBehaviour
    {
        [SerializeField] private Image healthBar;

        private void Start()
        {
            GameManager.Instance.PlayerHealth.onDamage.AddListener(h => healthBar.fillAmount = h.Value / 100f);
        }
    }
}