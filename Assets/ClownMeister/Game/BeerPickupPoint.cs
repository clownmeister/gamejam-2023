using System;
using UnityEngine;

namespace ClownMeister.Game
{
    public class BeerPickupPoint : MonoBehaviour
    {
        public int scorePointsAwarded = 0;
        public int energyAwarded = 0;
        public GameObject particlePrefabPickup;

        private void OnTriggerEnter(Collider other)
        {
            Debug.Log("Entered trigger "+ other.tag);
            if (!other.CompareTag("Player")) return;
            GameManager.Instance.AddPoints(this.scorePointsAwarded);
            GameManager.Instance.AddEnergy(this.energyAwarded);
            GameManager.BeerList.Remove(gameObject);
            Instantiate(this.particlePrefabPickup, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}