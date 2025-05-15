using UnityEngine;
using System.Collections;

public class GolpeArma : MonoBehaviour
{
    private bool puedeGolpear = false;
    [SerializeField] float duracionGolpe = 2.0f;
    public GenVillalobos player;
    private Vector3 posicionObjInicial;
    public void ActivarGolpe()
    {
        puedeGolpear = true;

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
            player.SoltarObjeto();
            transform.position = posicionObjInicial;
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
            // Volver a la posici�n original
            
            puedeGolpear = false;
        }
    }
    public void GuardarPosicionInicial()
    {
        posicionObjInicial = transform.position;
    }

}
