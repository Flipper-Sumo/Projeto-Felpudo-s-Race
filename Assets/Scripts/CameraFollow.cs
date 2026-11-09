using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform Player;
    [SerializeField] private Vector3 offset = new Vector3(0, 0, -10f);

    private void LateUpdate()
    {
        if (Player != null)
        {
            transform.position = new Vector3(Player.position.x + offset.x, transform.position.y, offset.z);
        }
    }
}