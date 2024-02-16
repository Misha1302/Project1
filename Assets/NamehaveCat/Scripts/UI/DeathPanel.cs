namespace NamehaveCat.Scripts.UI
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;
    using UnityEngine.UI;

    [RequireComponent(typeof(Image))]
    public class DeathPanel : MonoBehaviour
    {
        [SerializeField] private List<Transform> objects;

        private Image _image;

        public float Alpha
        {
            get => _image.color.a;
            set
            {
                var color = _image.color;
                color.a = Math.Clamp(value, 0, 1f);
                _image.color = color;
            }
        }

        private void Start()
        {
            _image = GetComponent<Image>();
            _image.raycastTarget = false;
            objects.ForEach(x => x.gameObject.SetActive(false));
        }

        private void OnValidate()
        {
            if (objects == null || objects.Count == 0)
                objects = GetComponentsInChildren<Transform>(true).Except(new[] { transform }).ToList();
        }

        public void BlockUI()
        {
            _image.raycastTarget = true;
            objects.ForEach(x => x.gameObject.SetActive(true));
        }
    }
}