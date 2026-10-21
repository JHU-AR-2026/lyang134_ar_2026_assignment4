using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;

    private Vector3 offset;

    private void Start()
    {
        if (player == null)
        {
            Debug.LogError("Assign the Player in CameraFollow.", this);
            enabled = false;
            return;
        }

        offset = transform.position - player.position;
    }

    private void LateUpdate()
    {
        if (player != null)
        {
            transform.position = player.position + offset;
        }
    }
}