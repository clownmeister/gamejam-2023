using System.Collections.Generic;
using ClownMeister.Ai;
using UnityEngine;
using Random = UnityEngine.Random;

namespace ClownMeister.Navigation
{
    public class VehicleManager : MonoBehaviour
    {
        private static readonly List<GameObject> Cars = new();

        public float spawnCooldown;
        private float nextSpawn = 0;
        public float targetVehicleCount;

        public Transform hierarchyParent;
        public List<VehicleNode> spawnPoints;
        public List<GameObject> carPrefabs;

        private void Update()
        {
            if (Cars.Count >= this.targetVehicleCount) return;
            if (!(this.nextSpawn < Time.time)) return;
            this.nextSpawn = Time.time + this.spawnCooldown;
            Spawn();
        }

        public static void RemoveCar(GameObject car)
        {
            if (Cars.Contains(car)) {
                return;
            }
            Cars.Remove(car);
        }

        public static void AddCar(GameObject car)
        {
            if (Cars.Contains(car)) {
                return;
            }
            Cars.Add(car);
        }

        private void Spawn()
        {
            GameObject car = this.carPrefabs[Random.Range(0, this.carPrefabs.Count)];
            VehicleNode spawn = this.spawnPoints[Random.Range(0, this.spawnPoints.Count)];

            GameObject carInstance = Instantiate(car, spawn.transform.position, spawn.transform.rotation, hierarchyParent);
            AddCar(carInstance);
        }
    }
}