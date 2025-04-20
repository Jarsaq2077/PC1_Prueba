using UnityEngine;
using UnityEngine.SceneManagement;

public class Llegada : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Invoke("VolverAJugar", 5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void VolverAJugar()
    {
        SceneManager.LoadScene("Principal"); // Cambia "Principal" por el nombre exacto de tu escena principal
    }
}
