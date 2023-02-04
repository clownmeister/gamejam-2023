using UnityEngine;

namespace ClownMeister.Player
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(InputHandler))]
    public class CharacterController : MonoBehaviour
    {
        [SerializeField] private bool rotateTowardMouse;

        [SerializeField] private float movementSpeed;
        [SerializeField] private float rotationSpeed;
        [SerializeField] private float jumpHeight;

        [SerializeField] private UnityEngine.Camera mainCamera;

        private InputHandler input;
        private Rigidbody body;
        private bool canJump;

        private void Awake()
        {
            this.input = GetComponent<InputHandler>();
            this.body = GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            Vector3 targetVector = new(this.input.InputVector.x, 0, this.input.InputVector.y);
            Vector3 movementVector = MoveTowardTarget(targetVector);

            if (!this.rotateTowardMouse) RotateTowardMovementVector(movementVector);
            if (this.rotateTowardMouse) RotateFromMouseVector();

            if (Input.GetButtonDown("Jump")) Jump();
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

        private void Jump()
        {
            Debug.Log("jump");
            if (!this.canJump) CheckGroundStatus();
            if (!this.canJump) return;

            this.canJump = false;
            this.body.AddForce(0, this.jumpHeight * this.body.mass, 0, ForceMode.Impulse);
        }

        private void CheckGroundStatus()
        {
            if (!Physics.Raycast(new Ray(transform.position, Vector3.down), out RaycastHit hit, 5f)) {
                this.canJump = false;
                return;
            }

            this.canJump = hit.collider != null;
        }
    }
}