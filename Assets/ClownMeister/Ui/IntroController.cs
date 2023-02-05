using System;
using System.Collections.Generic;
using ClownMeister.Input;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ClownMeister.Ui
{

    [RequireComponent(typeof(InputManager))]
    public class IntroController : MonoBehaviour
    {
        public List<AudioSource> audioSources;

        private int currentStep = 0;
        private InputManager inputManager;

        private const float Cooldown = 5;
        private const float NextSceneCooldown = 3;
        private bool loadNextScene = false;
        private float nextPageAt;
        private float nextSceneAt;

        private bool animationFinished;

        public List<Animator> animators;
        private static readonly int Play = Animator.StringToHash("Play");
        private static readonly int Stop = Animator.StringToHash("Stop");

        [Serializable]
        public struct PageSound {
            public int id;
            public AudioClip sound;
        }
        public PageSound[] sounds;

        private void Awake()
        {
            this.inputManager = GetComponent<InputManager>();
        }

        private void Start()
        {
            foreach (Animator animator in this.animators) {
                animator.transform.position = new Vector3(2000, 0, 0);
            }
        }


        private void PlaySound(int index)
        {
            foreach (PageSound pageSound in this.sounds) {
                if (index == pageSound.id) {
                    audioSources[index].clip = pageSound.sound;
                    audioSources[index].PlayDelayed(1);
                }
            }
        }

        // 0-2 slides
        // 3 new page
        // 4-6 slides
        // 7 slides out a load scene
        private void Update()
        {
            Debug.Log(this.currentStep  );
            if (this.loadNextScene && this.nextSceneAt < Time.time) {
                SceneManager.LoadScene(1);
            }
            Debug.Log(this.nextPageAt < Time.time);
            Debug.Log(this.inputManager.Jump);
            Debug.Log(this.currentStep);

            if (this.nextPageAt < Time.time && this.inputManager.Jump && this.currentStep <= 7) {
                Debug.Log(this.currentStep);
                if (this.currentStep == 3) {
                    for (int i = 0; i < 3; i++) {
                        this.animators[i].SetTrigger(Stop);
                    }

                    this.currentStep++;
                    return;
                }

                if (this.currentStep == 7 && !this.animationFinished) {
                    this.animationFinished = true;
                    for (int i = 3; i < 6; i++) {
                        this.animators[i].SetTrigger(Stop);
                    }

                    this.currentStep++;
                    LoadNextScene();
                    return;
                }

                int correctIndex = this.currentStep;
                if (this.currentStep > 2) {
                    correctIndex--;
                }

                if (this.currentStep > 7) {
                    return;
                }

                PlaySound(correctIndex);
                this.animators[correctIndex].enabled = true;
                this.animators[correctIndex].SetTrigger(Play);
                this.nextPageAt = Time.time + Cooldown;
                this.currentStep++;
            }
        }

        private void LoadNextScene()
        {
            this.loadNextScene = true;
            this.nextSceneAt = Time.time + NextSceneCooldown;
        }
    }
}