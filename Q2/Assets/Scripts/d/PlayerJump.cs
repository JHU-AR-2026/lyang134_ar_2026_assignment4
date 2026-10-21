using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    public float jumpSpeed = 6f;

    private Rigidbody rb;
    private SphereCollider sphere;
    private PlayerController movement;
    private bool jumpRequested;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        sphere = GetComponent<SphereCollider>();
        movement = GetComponent<PlayerController>();

        if (rb == null || sphere == null || movement == null)
        {
            Debug.LogError(
                "Attach PlayerJump to the ball with Rigidbody, " +
                "SphereCollider, and PlayerController.",
                this
            );

            enabled = false;
        }
    }

    private void Update()
    {
        // GameStart disables movement and makes the ball kinematic
        // while the game is waiting to start.
        if (rb.isKinematic || !movement.enabled)
        {
            jumpRequested = false;
            return;
        }

        if (Input.GetButtonDown("Jump"))
        {
            jumpRequested = true;
        }
    }

    private void FixedUpdate()
    {
        if (!jumpRequested)
        {
            return;
        }

        // Consume this keypress, even if jumping is not allowed.
        jumpRequested = false;

        if (rb.isKinematic || !movement.enabled || !IsGrounded())
        {
            return;
        }

        // Change vertical speed without removing horizontal movement.
        Vector3 velocity = rb.linearVelocity;
        velocity.y = jumpSpeed;
        rb.linearVelocity = velocity;
    }

    private bool IsGrounded()
    {
        // Do not allow another jump while moving upward.
        if (rb.linearVelocity.y > 0.1f)
        {
            return false;
        }

        // Look just below the ball, ignoring collectible triggers.
        return Physics.Raycast(
            sphere.bounds.center,
            Vector3.down,
            out RaycastHit hit,
            sphere.bounds.extents.y + 0.05f,
            Physics.DefaultRaycastLayers,
            QueryTriggerInteraction.Ignore
        ) && hit.normal.y > 0.5f;
    }
}