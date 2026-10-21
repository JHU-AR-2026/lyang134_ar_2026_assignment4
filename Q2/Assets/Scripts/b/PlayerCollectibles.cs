using UnityEngine;

public class PlayerCollectibles : MonoBehaviour
{
    public int collectedCount = 0;

    private void Start()
    {
        collectedCount = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Collectible"))
        {
            return;
        }

        // Avoid counting the same pickup more than once.
        if (!other.gameObject.activeSelf)
        {
            return;
        }

        other.gameObject.SetActive(false);
        collectedCount++;

        Debug.Log("Collected: " + collectedCount);
    }
}