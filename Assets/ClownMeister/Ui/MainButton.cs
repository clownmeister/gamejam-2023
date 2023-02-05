using System;
using UnityEngine;

namespace ClownMeister.Ui
{
    [RequireComponent(typeof(AudioSource))]
    public class MainButton : MonoBehaviour
    {
        private AudioSource audioSource;
        private AudioClip hoverSound;
        private AudioClip clickSound;

        private void Start()
        {
            this.audioSource = GetComponent<AudioSource>();
        }

        private void OnMouseOver()
        {
            this.audioSource.PlayOneShot(this.hoverSound);
        }

        private void OnMouseDown()
        {
            this.audioSource.PlayOneShot(this.clickSound);
        }

    }
}
