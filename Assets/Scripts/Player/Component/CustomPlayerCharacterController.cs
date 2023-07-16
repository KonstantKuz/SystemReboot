using Extension;
using UnityEngine;
using UnityFPS.Scripts;
using UnityFPS.Scripts.WallRunLevel;

namespace Player.Component
{
    [RequireComponent(typeof(CharacterController), typeof(PlayerInputHandler), typeof(AudioSource))]
    public class CustomPlayerCharacterController : MonoBehaviour
    {
        [Header("References")]
        [Tooltip("Reference to the main camera used for the player")]
        public Camera playerCamera;

        [Header("General")]
        [Tooltip("Force applied downward when in the air")]
        public float gravityDownForce = 20f;
        [Tooltip("Physic layers checked to consider the player grounded")]
        public LayerMask groundCheckLayers = -1;
        [Tooltip("distance from the bottom of the character controller capsule to test for grounded")]
        public float groundCheckDistance = 0.05f;

        [Header("Movement")]
        [Tooltip("Max movement speed when grounded (when not sprinting)")]
        public float maxSpeedOnGround = 10f;
        [Tooltip("Sharpness for the movement when grounded, a low value will make the player accelerate and decelerate slowly, a high value will do the opposite")]
        public float movementSharpnessOnGround = 15;
        [Tooltip("Max movement speed when not grounded")]
        public float maxSpeedInAir = 10f;
        [Tooltip("Acceleration speed when in the air")]
        public float accelerationSpeedInAir = 25f;

        [Header("Rotation")]
        [Tooltip("Rotation speed for moving the camera")]
        public float rotationSpeed = 200f;
        [Range(0.1f, 1f)]

        [Header("Jump")]
        [Tooltip("Force applied upward when jumping")]
        public float jumpForce = 9f;
        
        public Vector3 characterVelocity { get; set; }
        public bool isGrounded { get; private set; }
        public bool hasJumpedThisFrame { get; private set; }
        public float RotationMultiplier => 1f;

        PlayerInputHandler m_InputHandler;
        CharacterController m_Controller;
        Actor m_Actor;
        Vector3 m_GroundNormal;
        Vector3 m_CharacterVelocity;
        float m_LastTimeJumped = 0f;
        float m_CameraVerticalAngle = 0f;
        float m_footstepDistanceCounter;
        float m_TargetCharacterHeight;

        const float k_JumpGroundingPreventionTime = 0.1f;
        const float k_GroundCheckDistanceInAir = 0.07f;

        PlayerWallRunController wallRunComponent;

        void Start()
        {
            // fetch components on the same gameObject
            m_Controller = gameObject.RequireComponent<CharacterController>();
            m_InputHandler = gameObject.RequireComponent<PlayerInputHandler>();
            m_Actor = gameObject.RequireComponent<Actor>();
            wallRunComponent = gameObject.RequireComponent<PlayerWallRunController>();

            m_Controller.enableOverlapRecovery = true;
        }

        void Update()
        {
            hasJumpedThisFrame = false;

            GroundCheck();

            HandleCharacterMovement();
        }

        void GroundCheck()
        {
            // Make sure that the ground check distance while already in air is very small, to prevent suddenly snapping to ground
            float chosenGroundCheckDistance = isGrounded ? (m_Controller.skinWidth + groundCheckDistance) : k_GroundCheckDistanceInAir;

            // reset values before the ground check
            isGrounded = false;
            m_GroundNormal = Vector3.up;

            // only try to detect ground if it's been a short amount of time since last jump; otherwise we may snap to the ground instantly after we try jumping
            if (Time.time >= m_LastTimeJumped + k_JumpGroundingPreventionTime)
            {
                // if we're grounded, collect info about the ground normal with a downward capsule cast representing our character capsule
                if (Physics.CapsuleCast(GetCapsuleBottomHemisphere(), GetCapsuleTopHemisphere(m_Controller.height), m_Controller.radius, Vector3.down, out RaycastHit hit, chosenGroundCheckDistance, groundCheckLayers, QueryTriggerInteraction.Ignore))
                {
                    // storing the upward direction for the surface found
                    m_GroundNormal = hit.normal;

                    // Only consider this a valid ground hit if the ground normal goes in the same direction as the character up
                    // and if the slope angle is lower than the character controller's limit
                    if (Vector3.Dot(hit.normal, transform.up) > 0f &&
                        IsNormalUnderSlopeLimit(m_GroundNormal))
                    {
                        isGrounded = true;

                        // handle snapping to the ground
                        if (hit.distance > m_Controller.skinWidth)
                        {
                            m_Controller.Move(Vector3.down * hit.distance);
                        }
                    }
                }
            }
        }

        void HandleCharacterMovement()
        {
            // horizontal character rotation
            {
                // rotate the transform with the input speed around its local Y axis
                transform.Rotate(new Vector3(0f, (m_InputHandler.GetLookInputsHorizontal() * rotationSpeed * RotationMultiplier), 0f), Space.Self);
            }

            // vertical camera rotation
            {
                // add vertical inputs to the camera's vertical angle
                m_CameraVerticalAngle += m_InputHandler.GetLookInputsVertical() * rotationSpeed * RotationMultiplier;

                // limit the camera's vertical angle to min/max
                m_CameraVerticalAngle = Mathf.Clamp(m_CameraVerticalAngle, -89f, 89f);

                // apply the vertical angle as a local rotation to the camera transform along its right axis (makes it pivot up and down)
                if(wallRunComponent != null)
                {
                    playerCamera.transform.localEulerAngles = new Vector3(m_CameraVerticalAngle, 0, wallRunComponent.GetCameraRoll());
                }
                else
                {
                    playerCamera.transform.localEulerAngles = new Vector3(m_CameraVerticalAngle, 0, 0);
                }
            }

            // character movement handling
            {
                // converts move input to a worldspace vector based on our character's transform orientation
                Vector3 worldspaceMoveInput = transform.TransformVector(m_InputHandler.GetMoveInput());

                // handle grounded movement
                if (isGrounded || (wallRunComponent != null && wallRunComponent.IsWallRunning()))
                {
                    if (isGrounded)
                    {
                        // calculate the desired velocity from inputs, max speed, and current slope
                        Vector3 targetVelocity = worldspaceMoveInput * maxSpeedOnGround;
                        // reduce speed if crouching by crouch speed ratio
                        targetVelocity = GetDirectionReorientedOnSlope(targetVelocity.normalized, m_GroundNormal) * targetVelocity.magnitude;

                        // smoothly interpolate between our current velocity and the target velocity based on acceleration speed
                        characterVelocity = Vector3.Lerp(characterVelocity, targetVelocity, movementSharpnessOnGround * Time.deltaTime);
                    }
                    // jumping
                    if ((isGrounded || (wallRunComponent != null && wallRunComponent.IsWallRunning())) && m_InputHandler.GetJumpInputDown())
                    {
                        Jump();
                    }
                }
                // handle air movement
                else
                {   
                    if(wallRunComponent == null || (wallRunComponent!= null && !wallRunComponent.IsWallRunning()))
                    {
                        // add air acceleration
                        characterVelocity += worldspaceMoveInput * accelerationSpeedInAir * Time.deltaTime;

                        // limit air speed to a maximum, but only horizontally
                        float verticalVelocity = characterVelocity.y;
                        Vector3 horizontalVelocity = Vector3.ProjectOnPlane(characterVelocity, Vector3.up);
                        horizontalVelocity = Vector3.ClampMagnitude(horizontalVelocity, maxSpeedInAir);
                        characterVelocity = horizontalVelocity + (Vector3.up * verticalVelocity);

                        // apply the gravity to the velocity
                        characterVelocity += Vector3.down * gravityDownForce * Time.deltaTime;
                    }
                }
            }

            // apply the final calculated velocity value as a character movement
            Vector3 capsuleBottomBeforeMove = GetCapsuleBottomHemisphere();
            Vector3 capsuleTopBeforeMove = GetCapsuleTopHemisphere(m_Controller.height);
            m_Controller.Move(characterVelocity * Time.deltaTime);

            if (Physics.CapsuleCast(capsuleBottomBeforeMove, capsuleTopBeforeMove, m_Controller.radius, characterVelocity.normalized, out RaycastHit hit, characterVelocity.magnitude * Time.deltaTime, -1, QueryTriggerInteraction.Ignore))
            {
                characterVelocity = Vector3.ProjectOnPlane(characterVelocity, hit.normal);
            }
        }

        public void Jump()
        {
            if (isGrounded)
            {
                // start by canceling out the vertical component of our velocity
                characterVelocity = new Vector3(characterVelocity.x, 0f, characterVelocity.z);
                // then, add the jumpSpeed value upwards
                characterVelocity += Vector3.up * jumpForce;
            }
            else
            {
                characterVelocity = new Vector3(characterVelocity.x, 0f, characterVelocity.z);
                // then, add the jumpSpeed value upwards
                characterVelocity += wallRunComponent.GetWallJumpDirection() * jumpForce;
            }

            // remember last time we jumped because we need to prevent snapping to ground for a short time
            m_LastTimeJumped = Time.time;
            hasJumpedThisFrame = true;

            // Force grounding to false
            isGrounded = false;
            m_GroundNormal = Vector3.up;
        }

        // Returns true if the slope angle represented by the given normal is under the slope angle limit of the character controller
        bool IsNormalUnderSlopeLimit(Vector3 normal)
        {
            return Vector3.Angle(transform.up, normal) <= m_Controller.slopeLimit;
        }

        // Gets the center point of the bottom hemisphere of the character controller capsule    
        Vector3 GetCapsuleBottomHemisphere()
        {
            return transform.position + (transform.up * m_Controller.radius);
        }

        // Gets the center point of the top hemisphere of the character controller capsule    
        Vector3 GetCapsuleTopHemisphere(float atHeight)
        {
            return transform.position + (transform.up * (atHeight - m_Controller.radius));
        }

        // Gets a reoriented direction that is tangent to a given slope
        public Vector3 GetDirectionReorientedOnSlope(Vector3 direction, Vector3 slopeNormal)
        {
            Vector3 directionRight = Vector3.Cross(direction, transform.up);
            return Vector3.Cross(slopeNormal, directionRight).normalized;
        }
    }
}