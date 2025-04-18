using UnityEngine;

public class MoverCamara : MonoBehaviour
{
    public GameObject player;
    void Update()
    {
        Vector3 position = transform.position;
        position.x = player.transform.position.x;
        transform.position = position;
    }
}
