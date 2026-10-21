using UnityEngine;

public class BreakableWall : MonoBehaviour
{
    public bool IsBroken { get; private set; }

    private void OnCollisionEnter(Collision collision)
    {
        TryBreak(collision);
    }

    private void OnCollisionStay(Collision collision)
    {
        TryBreak(collision);
    }

    private void TryBreak(Collision collision)
    {
        if (!enabled || IsBroken || collision.rigidbody == null)
        {
            return;
        }

        PlayerPowerUp powerUp =
            collision.rigidbody.GetComponent<PlayerPowerUp>();

        if (powerUp == null || !powerUp.IsPoweredUp)
        {
            return;
        }

        IsBroken = true;

        Debug.Log("Wall broken!", this);

        // Hide the wall and remove its collision barrier.
        gameObject.SetActive(false);
    }
}