using UnityEngine;
using UnityEngine.SceneManagement;

public class Llegada : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            SceneManager.LoadScene("Principal");
        }
    }
    /*
    void Start()
    {
        Invoke("VolverAJugar", 5f);
    }

    void VolverAJugar()
    {
        SceneManager.LoadScene("Principal");
    }
    */
}
