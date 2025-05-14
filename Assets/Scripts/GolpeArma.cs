using UnityEngine;
using System.Collections;

public class GolpeArma : MonoBehaviour
{
    private bool puedeGolpear = false;
    [SerializeField] float duracionGolpe = 2.0f;

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
            Destroy(gameObject);
        }
    }
}
