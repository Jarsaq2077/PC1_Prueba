using UnityEngine;

public class FondoFijo : MonoBehaviour
{
    public GameObject player;
    
    void Update()
    {
        Vector3 position = transform.position;
        position.x = player.transform.position.x;
        position.y = player.transform.position.y;
        transform.position = position;
    }
}
