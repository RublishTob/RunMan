using System.Collections;
using UnityEngine;

namespace ReadyPlayerMe.Samples.QuickStart
{
    [RequireComponent(typeof(CharacterController), typeof(GroundCheck))]
    public class ThirdPersonMovement : MonoBehaviour
    {
        private const float TURN_SMOOTH_TIME = 0.05f;

        [SerializeField] private Camera _camera;
        [SerializeField] private Transform _left;
        [SerializeField] private Transform _right;


        [SerializeField][Tooltip("Used to determine movement direction based on input and camera forward axis")] 
        private Transform playerCamera;
        [SerializeField][Tooltip("Move speed of the character in")]
        private float walkSpeed = 3f;
        [SerializeField][Tooltip("Run speed of the character")] 
        private float runSpeed = 8f;
        [SerializeField][Tooltip("The character uses its own gravity value. The engine default is -9.81f")] 
        private float gravity = -18f;
        [SerializeField][Tooltip("The height the player can jump ")] 
        private float jumpHeight = 3f;

        private CharacterController controller;
        private GameObject avatar;
        
        private float verticalVelocity;
        private float turnSmoothVelocity;

        private bool jumpTrigger;
        public float CurrentMoveSpeed { get; private set; }
        private bool isRunning;

        private GroundCheck groundCheck;
        
        private void Awake()
        {
            controller = GetComponent<CharacterController>();
            groundCheck = GetComponent<GroundCheck>();
        }

        public void Setup(GameObject target)
        {
            avatar = target;
            if (playerCamera == null)
            {
                playerCamera = _camera.transform;
            }
        }
        public void MoveToPoint(float inputX)
        {
            if (inputX == 0)
            {
                Move(inputX, 0);
            }
            if(inputX < 0)
            {
                //transform.position = Vector3.MoveTowards(transform.position, _left.position, walkSpeed * Time.deltaTime);
                StartCoroutine(MoveToP(_left.position, inputX));
            }
            if (inputX > 0)
            {
                //transform.position = Vector3.MoveTowards(transform.position, _right.position, walkSpeed * Time.deltaTime);
                StartCoroutine(MoveToP(_right.position, inputX));
            }
        }

        private IEnumerator MoveToP(Vector3 point, float inputX)
        {
            while (transform.position.x != point.x)
            {
                if (transform.position.x == point.x)
                {
                    yield break;
                }
                transform.position = Vector3.MoveTowards(transform.position, point, walkSpeed * Time.deltaTime);
                //Move(inputX, 0);
                yield return null;

            }
        }

        public void Move(float inputX, float inputY)
        {
            //var moveDirection = playerCamera.right * inputX + playerCamera.forward * inputY;
            var moveDirection = playerCamera.right * inputX;

            var moveSpeed = isRunning ? runSpeed: walkSpeed;

            JumpAndGravity();
            controller.Move(moveDirection.normalized * (moveSpeed * Time.deltaTime) +  new Vector3(0.0f, verticalVelocity * Time.deltaTime, 0.0f));

            var moveMagnitude = moveDirection.magnitude;
            //CurrentMoveSpeed = isRunning ? runSpeed * moveMagnitude : walkSpeed * moveMagnitude;
            isRunning = true;
            CurrentMoveSpeed = runSpeed;

            if (moveMagnitude > 0)
            {
                RotateAvatarTowardsMoveDirection(moveDirection);
            }
            else
            {
                RotateAvatarTowardsMoveDirection(Vector3.forward);
            }
        }

        private void RotateAvatarTowardsMoveDirection(Vector3 moveDirection)
        {
            float targetAngle = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg + transform.rotation.y;
            float angle = Mathf.SmoothDampAngle(avatar.transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, TURN_SMOOTH_TIME);
            avatar.transform.rotation = Quaternion.Euler(0, angle, 0);
        }

        private void JumpAndGravity()
        {
            if (controller.isGrounded && verticalVelocity< 0)
            {
                verticalVelocity = -2f;
            }
            
            if (jumpTrigger && controller.isGrounded)
            {
                verticalVelocity = Mathf.Sqrt(jumpHeight * -2f * gravity);
                jumpTrigger = false;
            }
            
            verticalVelocity += gravity * Time.deltaTime;
        }

        public void SetIsRunning(bool running)
        {
            isRunning = running;
        }
        
        public bool TryJump()
        {
            jumpTrigger = false;
            if (controller.isGrounded)
            {
                jumpTrigger = true;
            }
            return jumpTrigger;
        }

        public bool IsGrounded()
        {
            if (verticalVelocity > 0)
            {
                return false;
            }
            return groundCheck.IsGrounded();
        }
    }
}
