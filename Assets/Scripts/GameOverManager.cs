using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
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
        Invoke("VolverAJugar", 3f);
    }

    void VolverAJugar()
    {
        SceneManager.LoadScene("Principal");
    }
    */
}
