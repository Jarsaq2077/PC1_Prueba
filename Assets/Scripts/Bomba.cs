using UnityEngine;

public class Bomba : MonoBehaviour
{
    [Header("Configuración de Explosión")]
    [SerializeField] private float radioExplosion = 1.5f;
    [SerializeField] private LayerMask layerEnemigos;
    [SerializeField] private GameObject efectoExplosion;

    [Header("Temporizadores")]
    [SerializeField] private float tiempoParaAnimacion = 2f;
    [SerializeField] private float duracionAnimacion = 1.5f;

    private Animator animator;
    private bool yaExplotando = false;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void IniciarCuentaAtras()
    {
        if (!yaExplotando)
        {
            yaExplotando = true;
            Invoke(nameof(IniciarAnimacionExplosion), tiempoParaAnimacion);
        }
    }

    private void IniciarAnimacionExplosion()
    {
        if (animator != null)
        {
            animator.SetTrigger("Explode");
        }

        Invoke(nameof(HacerExplosion), duracionAnimacion);
    }

    private void HacerExplosion()
    {
        Collider2D[] enemigos = Physics2D.OverlapCircleAll(transform.position, radioExplosion, layerEnemigos);
        foreach (Collider2D enemigo in enemigos)
        {
            Debug.Log("Enemigo destruido: " + enemigo.name);
            Destroy(enemigo.gameObject);
        }

        if (efectoExplosion != null)
        {
            Instantiate(efectoExplosion, transform.position, Quaternion.identity);
        }

        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radioExplosion);
    }
}
