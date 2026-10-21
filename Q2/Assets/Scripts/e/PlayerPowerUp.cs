using TMPro;
using UnityEngine;

public class PlayerPowerUp : MonoBehaviour
{
    public GameObject powerUpCapsule;
    public TMP_Text powerUpText;
    public Material poweredMaterial;

    public int totalCollectibles = 8;
    public float forceMultiplier = 2f;

    public bool IsPoweredUp { get; private set; }

    private PlayerCollectibles collectibles;
    private PlayerController movement;
    private Renderer playerRenderer;
    private bool capsuleRevealed;

    private void Awake()
    {
        collectibles = GetComponent<PlayerCollectibles>();
        movement = GetComponent<PlayerController>();
        playerRenderer = GetComponent<Renderer>();

        if (collectibles == null || movement == null ||
            playerRenderer == null)
        {
            Debug.LogError(
                "Attach PlayerPowerUp to the actual player ball.",
                this
            );
            enabled = false;
            return;
        }

        if (powerUpCapsule == null || powerUpText == null ||
            poweredMaterial == null)
        {
            Debug.LogError(
                "Assign the capsule, text, and material in PlayerPowerUp.",
                this
            );
            enabled = false;
            return;
        }

        if (totalCollectibles < 1 || forceMultiplier <= 1f)
        {
            Debug.LogError(
                "Total Collectibles must be positive, and " +
                "Force Multiplier must be greater than 1.",
                this
            );
            enabled = false;
            return;
        }

        IsPoweredUp = false;
        capsuleRevealed = false;

        powerUpCapsule.SetActive(false);
        powerUpText.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (capsuleRevealed || IsPoweredUp || !movement.enabled)
        {
            return;
        }

        if (collectibles.collectedCount >= totalCollectibles)
        {
            capsuleRevealed = true;
            powerUpCapsule.SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!enabled || !movement.enabled || IsPoweredUp ||
            !capsuleRevealed || other.gameObject != powerUpCapsule)
        {
            return;
        }

        IsPoweredUp = true;

        playerRenderer.material = poweredMaterial;
        movement.moveForce *= forceMultiplier;

        powerUpText.text = "Power-up active!";
        powerUpText.gameObject.SetActive(true);

        powerUpCapsule.SetActive(false);

        Debug.Log("Power-up active!", this);
    }
}