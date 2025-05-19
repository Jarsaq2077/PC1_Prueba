using UnityEngine;
using System.Collections;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GolpeArma : MonoBehaviour
{
    private bool puedeGolpear = false;
    [SerializeField] float duracionGolpe = 2.0f;
    public GenVillalobos player;
    private Vector3 posicionObjInicial;
    private Animator animator;
    private float tiempoEspera;
    [SerializeField] int contadorEnemigos = 1;
    public AudioClip sonidoGolpe;
    private AudioSource audioSource;
    public void ActivarGolpe()
    {        
        puedeGolpear = true;
        animator = GetComponent<Animator>();
        tiempoEspera = duracionGolpe;
        animator.SetBool("golpe", true);
        StartCoroutine(DesactivarsGolpe(tiempoEspera));
        //StartCoroutine(DesactivarTrasTiempo());
    }

    /*private IEnumerator DesactivarTrasTiempo()
    {
        yield return new WaitForSeconds(duracionGolpe);
        puedeGolpear = false;
    }*/
        
     private void OnTriggerEnter2D(Collider2D collision)
     {
        if (puedeGolpear && collision.CompareTag("enemigo"))
        {
            
            Destroy(collision.gameObject);
            contadorEnemigos--;
            if(contadorEnemigos <= 0)
            {
                tiempoEspera = 0.5f;
                animator.SetBool("ruptura", true);
                StartCoroutine(DestruirObjeto(tiempoEspera));
            }                             
            /*transform.position = posicionObjInicial;
            transform.SetParent(null);
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.simulated = true;
            }

            Collider2D col = GetComponent<Collider2D>();
            if (col != null)
            {
                col.isTrigger = false;
                col.enabled = true;
            }
            // Volver a la posici�n original*/
                       
        }
    }
    private IEnumerator DesactivarsGolpe(float tiempoEspera)
    {
        yield return new WaitForSeconds(tiempoEspera);
        puedeGolpear = false;
        animator.SetBool("golpe", false);
    }
    private IEnumerator DestruirObjeto(float tiempoEspera)
    {
        yield return new WaitForSeconds(tiempoEspera);
        Destroy(gameObject);
        animator.SetBool("ruptura", false);
    }
    public void GuardarPosicionInicial()
    {
        posicionObjInicial = transform.position;
    }

}
