using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public AudioClip sonidoFondo;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (sonidoFondo != null)
        {
            audioSource.clip = sonidoFondo;
            audioSource.loop = true;
            audioSource.Play();
        }
    }
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
