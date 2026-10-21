using UnityEngine;

public class GameStart : MonoBehaviour
{
    public GameObject startButton;

    private Rigidbody rb;
    private PlayerController movement;
    private bool gameStarted;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        movement = GetComponent<PlayerController>();

        if (rb == null || movement == null)
        {
            Debug.LogError(
                "Attach GameStart to the ball with Rigidbody and PlayerController.",
                this
            );

            enabled = false;
            return;
        }

        gameStarted = false;

        // Keep the player still until Start is clicked.
        movement.enabled = false;
        rb.useGravity = false;
        rb.isKinematic = true;
    }

    public void StartGame()
    {
        if (!enabled || gameStarted)
        {
            return;
        }

        gameStarted = true;

        // Enable physics and keyboard movement.
        rb.isKinematic = false;
        rb.useGravity = true;
        movement.enabled = true;

        if (startButton != null)
        {
            startButton.SetActive(false);
        }
    }
}