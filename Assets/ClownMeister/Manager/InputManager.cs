using UnityEngine;

namespace ClownMeister.Manager
{
    public class InputManager : MonoBehaviour
    {
        public float scrollSensitivity = 1;
        public float scrollSensitivityController = .005f;

        public Vector2 InputVector { get; private set; }

        public Vector3 MousePosition { get; private set; }
        public float Scroll { get; private set; }
        public Vector2 Dpad { get; private set; }

        public bool Jump { get; private set; }
        // Update is called once per frame
        private void Update()
        {
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");
            InputVector = new Vector2(h, v);

            MousePosition = Input.mousePosition;

            Jump = Input.GetAxis("Jump") > 0;

            float dh = Input.GetAxis("DHorizontal");
            float dv = Input.GetAxis("DVertical");
            Dpad = new Vector2(dh, dv);

            Scroll = -Input.GetAxis("Mouse ScrollWheel");
        }
    }
}