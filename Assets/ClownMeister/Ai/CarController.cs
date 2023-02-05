using System;
using System.Collections.Generic;
using ClownMeister.Navigation;
using UnityEngine;
using Random = UnityEngine.Random;

namespace ClownMeister.Ai
{
    [RequireComponent(typeof(CarNavigator))]
    public class CarController : MonoBehaviour
    {
        public bool roaming = false;
        public Transform navAnchor;

        public VehicleNode nextTarget = null;
        public List<VehicleNode> path = null;
        public List<Light> lights;

        private CarNavigator navigator;

        private int stuckCount = 0;
        public int maxStuckCount = 3;

        private void Awake()
        {
            this.navigator = GetComponent<CarNavigator>();
        }

        private void Start()
        {
            if (this.roaming) {
                VehicleManager.AddCar(gameObject);
            }
            else {
                this.navigator.enabled = false;
                enabled = false;
                foreach (Light carLight in this.lights) {
                    carLight.enabled = false;
                }
            }
        }

        private void Update()
        {
            //stuck logic
            if (this.stuckCount >= this.maxStuckCount) {
                VehicleManager.RemoveCar(gameObject);
                Destroy(gameObject);
                return;
            }

            if (this.navigator.stuck) {
                this.stuckCount++;
                UpdateTarget();
            }

            //static car
            if (!this.roaming) return;

            //init
            if (this.nextTarget == null) {
                UpdateTarget();
            }

            //still navigating
            //main loop
            if (this.navigator.IsNavigating()) return;

            if (this.nextTarget.nodeType == VehicleNodeType.End) {
                VehicleManager.RemoveCar(gameObject);
                Destroy(gameObject);
                return;
            }

            this.stuckCount = 0;
            var available = this.nextTarget.traversableNeighbours;
            UpdateTarget(available[Random.Range(0,available.Count)]);
        }

        private void UpdateTarget(VehicleNode target = null)
        {
            this.nextTarget = target == null ? GetNewTarget() : target;
            this.navigator.SetDestination(this.nextTarget.transform.position, this.nextTarget.speedModifier);
        }

        private VehicleNode GetNewTarget()
        {
            return VehicleNodeManager.GetClosestNode(this.navAnchor.position);
        }
    }
}
