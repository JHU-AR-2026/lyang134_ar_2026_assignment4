using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    public float moveForce = 10f;

    private Rigidbody rb;
    private Vector3 movement;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Move along the floor, not vertically.
        movement = new Vector3(horizontal, 0f, vertical);

        // Prevent diagonal input from producing a stronger force.
        movement = Vector3.ClampMagnitude(movement, 1f);
    }

    private void FixedUpdate()
    {
        rb.AddForce(movement * moveForce);
    }
}