using UnityEngine;

namespace ClownMeister.Input
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
            float h = UnityEngine.Input.GetAxis("Horizontal");
            float v = UnityEngine.Input.GetAxis("Vertical");
            InputVector = new Vector2(h, v);

            MousePosition = UnityEngine.Input.mousePosition;

            Jump = UnityEngine.Input.GetAxis("Jump") > 0;

            float dh = UnityEngine.Input.GetAxis("DHorizontal");
            float dv = UnityEngine.Input.GetAxis("DVertical");
            Dpad = new Vector2(dh, dv);

            Scroll = UnityEngine.Input.GetAxis("Mouse ScrollWheel");
        }
    }
}