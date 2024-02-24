namespace NamehaveCat.Scripts.MImplementations
{
    using NamehaveCat.Scripts.Helpers;
    using UnityEngine;

    [RequireComponent(typeof(AudioSource))]
    public class MAudioSource : MonoBehaviour
    {
        private void Start()
        {
            var audioSource = GetComponent<AudioSource>();
            audioSource.volume = GameDynamicData.MusicValue.get();
            GameDynamicData.MusicValue.onSet.AddListener(value => audioSource.volume = value);
        }
    }
}