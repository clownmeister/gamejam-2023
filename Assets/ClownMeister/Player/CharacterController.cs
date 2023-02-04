using UnityEngine;

namespace ClownMeister.Player
{
    [RequireComponent(typeof(InputHandler))]
    public class CharacterController : MonoBehaviour
    {
        [SerializeField] private bool rotateTowardMouse;

        [SerializeField] private float movementSpeed;
        [SerializeField] private float rotationSpeed;

        [SerializeField] private UnityEngine.Camera mainCamera;
        private InputHandler input;

        private void Awake()
        {
            this.input = GetComponent<InputHandler>();
        }

        private void FixedUpdate()
        {
            Vector3 targetVector = new(this.input.InputVector.x, 0, this.input.InputVector.y);
            Vector3 movementVector = MoveTowardTarget(targetVector);

            if (!this.rotateTowardMouse) RotateTowardMovementVector(movementVector);
            if (this.rotateTowardMouse) RotateFromMouseVector();
        }

        private void RotateFromMouseVector()
        {
            Ray ray = this.mainCamera.ScreenPointToRay(this.input.MousePosition);

            if (!Physics.Raycast(ray, out RaycastHit hitInfo, 300f)) return;

            Vector3 target = hitInfo.point;
            target.y = transform.position.y;
            transform.LookAt(target);
        }

        private Vector3 MoveTowardTarget(Vector3 targetVector)
        {
            float speed = this.movementSpeed * Time.deltaTime;

            targetVector = Quaternion.Euler(0, this.mainCamera.gameObject.transform.rotation.eulerAngles.y, 0) * targetVector;
            Vector3 targetPosition = transform.position + targetVector * speed;
            transform.position = targetPosition;
            return targetVector;
        }

        private void RotateTowardMovementVector(Vector3 movementDirection)
        {
            if (movementDirection.magnitude == 0) return;
            Quaternion rotation = Quaternion.LookRotation(movementDirection);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, this.rotationSpeed);
        }
    }
}