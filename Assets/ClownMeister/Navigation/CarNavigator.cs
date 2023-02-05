using System;
using Unity.VisualScripting;
using UnityEngine;

namespace ClownMeister.Navigation
{
    [RequireComponent(typeof(Rigidbody))]
    public class CarNavigator : MonoBehaviour
    {
        [Header("Car settings")]
        public float speed = 1f;
        public float stoppingDistance;
        public float maxSteerAngle = 20;
        public float stuckCooldown = 5;
        public Transform navAnchor;

        public Light stopLightLeft;
        public Light stopLightRight;

        [Header("Wheel Colliders")]
        public WheelCollider frontLeftWheel;
        public WheelCollider frontRightWheel;
        public WheelCollider rearRightWheel;
        public WheelCollider rearLeftWheel;

        [Header("Wheel transform (set rear right only if same object)")]
        public Transform frontLeftWheelTransform;
        public Transform frontRightWheelTransform;
        public Transform rearRightWheelTransform;
        public Transform rearLeftWheelTransform;

        [HideInInspector]public float remainingDistance;
        [HideInInspector]public bool navigating;

        private float recalculateAt = 0;
        [HideInInspector]public bool stuck;

        private Rigidbody body;
        private Vector3 destination;
        private float currentSpeed = 1f;

        private void Awake()
        {
            this.body = GetComponent<Rigidbody>();
        }

        private void Start()
        {
            this.currentSpeed = this.speed;
        }

        private void FixedUpdate()
        {
            if (this.recalculateAt < Time.time) {
                this.stuck = true;
            }
            
            if (this.navigating == false) return;
            UpdateDistance();

            if (this.remainingDistance <= this.stoppingDistance) {
                this.navigating = false;
                return;
            }

            // TODO: good top speed regulator use this in the future for other projects
            // this.body.drag = (this.speed * Time.deltaTime * 1000 / this.topSpeed);
            // this.body.AddRelativeForce(Vector3.forward * (this.speed * Time.deltaTime * 1000), ForceMode.Acceleration);
            Steer();

            switch (this.currentSpeed) {
                case > 0:
                    Accelerate();
                    break;
                // case < 0:
                //     Brake();
                //     break;
            }

            UpdateWheelPoses();
        }

        private void Steer()
        {
            Vector3 relativeVector = this.navAnchor.InverseTransformPoint(this.destination);
            float newSteer = (relativeVector.x / relativeVector.magnitude) * this.maxSteerAngle;
            this.frontLeftWheel.steerAngle = newSteer;
            this.frontRightWheel.steerAngle = newSteer;
        }

        private void UpdateWheelPoses()
        {
            UpdateWheelPose(this.frontLeftWheel, this.frontLeftWheelTransform);
            UpdateWheelPose(this.frontRightWheel, this.frontRightWheelTransform);

            UpdateWheelPose(this.rearRightWheel, this.rearRightWheelTransform);
            if (this.rearLeftWheelTransform == this.rearRightWheelTransform) return;
            UpdateWheelPose(this.rearLeftWheel, this.rearLeftWheelTransform);
        }

        private void UpdateWheelPose(WheelCollider wheelCollider, Transform wheelTransform)
        {
            wheelCollider.GetWorldPose(out Vector3 position, out Quaternion quaternion);

            // wheelTransform.position = position;
            wheelTransform.rotation = quaternion;
        }

        private void Accelerate()
        {
            float idealSpeed = this.currentSpeed * this.body.mass;
            this.frontLeftWheel.motorTorque = idealSpeed;
            this.frontRightWheel.motorTorque = idealSpeed;

            //TODO: this belongs in controller
            this.stopLightLeft.enabled = false;
            this.stopLightRight.enabled = false;
        }

        private void Brake()
        {
            Debug.Log("Braking");
            float idealSpeed = this.currentSpeed * this.body.mass;
            this.frontLeftWheel.brakeTorque = idealSpeed;
            this.frontRightWheel.brakeTorque = idealSpeed;
            this.rearLeftWheel.brakeTorque = idealSpeed;
            this.rearRightWheel.brakeTorque = idealSpeed;

            //TODO: this belongs in controller
            this.stopLightLeft.enabled = true;
            this.stopLightRight.enabled = true;
        }

        private void UpdateDistance()
        {
            this.remainingDistance = (this.destination - this.navAnchor.transform.position).sqrMagnitude;
        }

        public void SetDestination(Vector3 position, float speedModifier = 1)
        {
            this.currentSpeed = this.speed * speedModifier;
            this.destination = position;
            this.navigating = true;
            this.recalculateAt = Time.time + this.stuckCooldown;
            this.stuck = false;
        }

        public bool IsNavigating()
        {
            return this.navigating;
        }
    }
}