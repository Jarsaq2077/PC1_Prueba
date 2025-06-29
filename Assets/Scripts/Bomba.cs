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

    [Header("Audio")]
    [SerializeField] private AudioClip sonidoExplosion;
    private AudioSource audioSource;

    private Animator animator;
    private bool yaExplotando = false;

    private void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
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
        // 🔥 Activar animación
        if (animator != null)
        {
            animator.SetTrigger("Explode");
        }

        // 🔊 Reproducir sonido al mismo tiempo
        if (audioSource != null && sonidoExplosion != null)
        {
            audioSource.PlayOneShot(sonidoExplosion);
        }

        // ⏳ Luego de la animación, hacer la explosión lógica
        Invoke(nameof(HacerExplosion), duracionAnimacion);
    }

    private void HacerExplosion()
    {
        Collider2D[] objetos = Physics2D.OverlapCircleAll(transform.position, radioExplosion);

        foreach (Collider2D objeto in objetos)
        {
            if (objeto.CompareTag("enemigo"))
            {
                Destroy(objeto.gameObject);
            }
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
