using UnityEngine;

namespace ClownMeister.Player
{
    public class InputHandler : MonoBehaviour
    {
        public Vector2 InputVector { get; private set; }

        public Vector3 MousePosition { get; private set; }

        // Update is called once per frame
        private void Update()
        {
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");
            InputVector = new Vector2(h, v);

            MousePosition = Input.mousePosition;
        }
    }
}