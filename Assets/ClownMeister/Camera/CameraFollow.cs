using System;
using ClownMeister.Input;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace ClownMeister.Camera
{
    public class CameraFollow : MonoBehaviour
    {
        public Transform target;
        public float smoothSpeed = .13f;
        public float zoomSensitivity = 1;
        [SerializeField]private float offsetMultiplier = .5f;

        public Vector3 offset;
        public Vector3 offsetMin = new Vector3(0, 5, -5);
        public Vector3 offsetMax = new Vector3(0, 30, -15);

        [SerializeField]private InputManager input;


        private void Update()
        {
            float scrollY = this.input.Scroll * this.input.scrollSensitivity + this.input.Dpad.y * this.input.scrollSensitivityController;

            if (scrollY == 0) return;
            float scrollOffset = this.offsetMultiplier + -scrollY * this.zoomSensitivity;
            this.offsetMultiplier = scrollOffset switch
            {
                < 0 => 0,
                > 1 => 1,
                _ => scrollOffset
            };
        }

        private void FixedUpdate()
        {
            this.offset = GetOffset();
            Vector3 desiredPosition = this.target.position + this.offset;
            Vector3 lerpedPosition = Vector3.Lerp(transform.position, desiredPosition, this.smoothSpeed);
            transform.position = lerpedPosition;

            transform.LookAt(this.target);
        }

        private Vector3 GetOffset()
        {
            return (this.offsetMax - this.offsetMin) * this.offsetMultiplier + this.offsetMin;
        }
    }
}