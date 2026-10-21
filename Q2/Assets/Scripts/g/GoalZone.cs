using TMPro;
using UnityEngine;

public class GoalZone : MonoBehaviour
{
    public GameObject player;
    public TMP_Text winText;
    public BreakableWall requiredWall;

    private Rigidbody playerBody;
    private PlayerPowerUp powerUp;
    private bool hasWon;

    private void Awake()
    {
        if (player == null || winText == null || requiredWall == null)
        {
            Debug.LogError(
                "Assign Player, Win Text, and Required Wall in GoalZone.",
                this
            );

            enabled = false;
            return;
        }

        playerBody = player.GetComponent<Rigidbody>();
        powerUp = player.GetComponent<PlayerPowerUp>();

        if (playerBody == null || powerUp == null)
        {
            Debug.LogError(
                "Assign the actual player ball with Rigidbody " +
                "and PlayerPowerUp.",
                this
            );

            enabled = false;
            return;
        }

        hasWon = false;
        winText.text = "You win!";
        winText.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!enabled || hasWon ||
            other.attachedRigidbody != playerBody)
        {
            return;
        }

        // Require the intended sequence before allowing a win.
        if (!powerUp.IsPoweredUp || !requiredWall.IsBroken)
        {
            return;
        }

        hasWon = true;

        if (powerUp.powerUpText != null)
        {
            powerUp.powerUpText.gameObject.SetActive(false);
        }

        winText.gameObject.SetActive(true);
        player.SetActive(false);

        Debug.Log("You win!", this);
    }
}