using System;
using UnityEngine;

namespace ClownMeister.Camera
{
    [ExecuteInEditMode]
    public class Billboard : MonoBehaviour
    {
        private UnityEngine.Camera mainCamera;

        public float damping = 3;
        private void Start()
        {
            this.mainCamera = UnityEngine.Camera.main;
        }

        private void Update()
        {
            Vector3 camPosition = this.mainCamera.transform.position;
            Vector3 lookPos = camPosition - transform.position;
            Quaternion rotation = Quaternion.LookRotation(-lookPos);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * this.damping);
        }
    }
}
