using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    void Start()
    {
        // Espera 3 segundos y luego carga la escena principal
        Invoke("VolverAJugar", 3f);
    }

    void VolverAJugar()
    {
        SceneManager.LoadScene("Principal"); // Cambia "Principal" por el nombre exacto de tu escena principal
    }
}
