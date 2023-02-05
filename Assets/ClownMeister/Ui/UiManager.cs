using System.Globalization;
using TMPro;
using UnityEngine;
using Image = UnityEngine.UI.Image;

namespace ClownMeister.Ui
{
    [RequireComponent(typeof(GameManager))]
    public class UiManager : MonoBehaviour
    {
        public TextMeshProUGUI scoreText;
        public Animator scoreAnimator;
        public TextMeshProUGUI multiplierText;
        public Animator multiplierAnimator;
        public Image energyBar;

        private GameManager gameManager;

        private int oldMulti;
        private int oldScore;

        private float barValue = 1;
        private static readonly int Zoom = Animator.StringToHash("Zoom");

        private void Awake()
        {
            this.gameManager = GetComponent<GameManager>();
        }

        private void Start()
        {
            UpdateScores();
        }

        private void Update()
        {
            float newValue = this.gameManager.energy / GameManager.MaxEnergy;
            this.barValue = Mathf.Lerp(this.barValue, newValue, Time.deltaTime * this.gameManager.energyTickSeconds);
            this.energyBar.fillAmount = this.barValue;

            UpdateScores();
        }

        private void UpdateScores()
        {
            int score = this.gameManager.score;
            int multi = this.gameManager.multiplier;

            if (score != this.oldScore) {
                this.scoreAnimator.SetTrigger(Zoom);
            }
            if (multi != this.oldMulti) {
                this.multiplierAnimator.SetTrigger(Zoom);
            }
            
            this.scoreText.SetText(score.ToString(CultureInfo.CurrentCulture));
            this.multiplierText.SetText(multi + "X");

            this.oldScore = score;
            this.oldMulti = multi;
        }
    }
}