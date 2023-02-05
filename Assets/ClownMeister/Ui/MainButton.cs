using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ClownMeister.Ui
{
    [RequireComponent(typeof(AudioSource))]
    public class MainButton : MonoBehaviour
    {
        private AudioSource audioSource;
        public AudioClip hoverSound;
        public AudioClip clickSound;

        private void Start()
        {
            this.audioSource = GetComponent<AudioSource>();
        }

        public void Hover()
        {
            this.audioSource.PlayOneShot(this.hoverSound);
        }

        public void Click()
        {
            this.audioSource.PlayOneShot(this.clickSound);
        }

        public static void Exit()
        {
            Application.Quit();
        }

        public static void LoadGame()
        {
            SceneManager.LoadScene(2);
        }

    }
}
