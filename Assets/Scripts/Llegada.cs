using UnityEngine;
using UnityEngine.SceneManagement;

public class Llegada : MonoBehaviour
{
    public AudioClip sonidoFondo;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (sonidoFondo != null)
        {
            audioSource.clip = sonidoFondo;
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
        Invoke("VolverAJugar", 5f);
    }

    void VolverAJugar()
    {
        SceneManager.LoadScene("Principal");
    }
    */
}
