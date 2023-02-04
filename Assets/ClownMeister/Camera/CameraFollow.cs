using UnityEngine;

namespace ClownMeister.Camera
{
    public class CameraFollow : MonoBehaviour
    {
        public Transform target;
        public float smoothSpeed = .13f;
        public Vector3 offset = new Vector3(0, 15, -10);

        private void FixedUpdate()
        {
            Vector3 desiredPosition = this.target.position + this.offset;
            Vector3 lerpedPosition = Vector3.Lerp(transform.position, desiredPosition, this.smoothSpeed);
            transform.position = lerpedPosition;

            transform.LookAt(this.target);
        }
    }
}