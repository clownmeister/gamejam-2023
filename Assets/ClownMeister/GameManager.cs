using System;
using ClownMeister.Player;
using Unity.VisualScripting;
using UnityEngine;

namespace ClownMeister
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;
        public PlayerController player;
        public bool gameOver;

        [HideInInspector]public float energy;
        public float energyConsumptionPerSecond = 5;
        public float energyTickSeconds = 3;
        private float nextEnergyConsumption = 0;

        public int score = 0;
        public int scoreChained = 0;
        public float multiplierResetAt = 0;
        public int multiplier = 1;

        public const int MultiplierPerScore = 300;
        public const float MultiplierChainCooldown = 5;
        public const int MaxMultiplier = 8;
        public const float MaxEnergy = 100;

        private void Start()
        {
            Instance = this;
            this.energy = MaxEnergy;
        }

        private void Update()
        {
            if (this.nextEnergyConsumption < Time.time) {
                this.nextEnergyConsumption = Time.time + this.energyTickSeconds;
                float amount = this.energy - this.energyConsumptionPerSecond;
                this.energy = amount < 0 ? 0 : amount;
            }

            if (this.energy <= 0) {
                GameOver();
                this.gameOver = true;
            }
        }

        private void GameOver()
        {
            Debug.Log("Game over!");
        }

        public void AddPoints(int amount)
        {
            if (this.multiplierResetAt <= Time.time) {
                this.scoreChained = 0;
            }
            
            this.multiplierResetAt = Time.time + MultiplierChainCooldown;
            this.scoreChained += amount;
            this.score += amount;

            int multiplierCalculated = this.scoreChained / MultiplierPerScore;
            this.multiplier = multiplierCalculated switch
            {
                < 1 => 1,
                > MaxMultiplier => MaxMultiplier,
                _ => multiplierCalculated
            };
        }

        public void AddEnergy(float amount)
        {
            float energyGained = amount + this.energy;
            this.energy = energyGained switch
            {
                < 0 => 0,
                > MaxEnergy => MaxEnergy,
                _ => energyGained
            };
        }
    }
}