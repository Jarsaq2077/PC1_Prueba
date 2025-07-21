using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BossEsqueletor : MonoBehaviour
{
    public Vector3 boxSize = new Vector3(2f, 2f, 2f); // Tamaño del área
    public Vector3 boxOffset = new Vector3(0f, 1f, 2f); // Offset respecto al objeto
    public LayerMask detectionLayer; // Capa del objeto a detectar
    private Animator Animator;
    public GenVillalobos player;
    [SerializeField] List<Transform> wayPoints;
    [SerializeField] float speed = 20;
    [SerializeField] float distanciaCambio = 0.2f;
    [SerializeField] float offsetAlSuelo = 0.5f;
    [SerializeField] int vidas;
    bool yagolpeo;
    bool ataqueActivo;
    public Collider2D colliderAtaque;
    byte siguientePosicion = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Animator = GetComponent<Animator>();
        colliderAtaque.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, wayPoints[siguientePosicion].transform.position, speed * Time.deltaTime);
        if (siguientePosicion == 1)
        {
            transform.localScale = new Vector3(-1.0f, 1.0f, 10f);
        }
        else
        {
            transform.localScale = new Vector3(1.0f, 1.0f, 10f);
        }
        if (Vector3.Distance(transform.position, wayPoints[siguientePosicion].transform.position) < distanciaCambio)
        {
            siguientePosicion++;
            if (siguientePosicion >= wayPoints.Count)
            {
                siguientePosicion = 0;
            }
        }

        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 5f);
        if (hit.collider != null)
        {
            if (hit.collider.CompareTag("Ground") || hit.collider.name.ToLower().Contains("suelo"))
            {
                Vector3 nuevaPos = transform.position;
                nuevaPos.y = hit.point.y + offsetAlSuelo;
                transform.position = nuevaPos;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Animator.SetBool("pegar", true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Animator.SetBool("pegar", false);
        }
    }
    public void ActivarColliderAtaque()
    {
        ataqueActivo = true;
        yagolpeo = false;
        colliderAtaque.enabled = true;
    }

    public void DesactivarColliderAtaque()
    {
        ataqueActivo = false;
        colliderAtaque.enabled = false;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (ataqueActivo && !yagolpeo && collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("¡Jugador golpeado!");
            player.vidas--;
            player.ActualizarVidas();
            if (player.vidas <= 0)
            {
                SceneManager.LoadScene("GameOver");
            }
            yagolpeo = true;
        }
    }
    public void recibirDanio()
    {
        if (vidas <= 0)
        {
            Animator.SetBool("dead", true);
            StartCoroutine(EsperarAntesDeMorir());
        }
        else
        {
            Animator.SetBool("hurt", true);
            StartCoroutine(EsperarAntesDeQuitarHurt());
            vidas--;
        }
    }
    private IEnumerator EsperarAntesDeQuitarHurt()
    {
        yield return new WaitForSeconds(0.5f); 
        Animator.SetBool("hurt", false);
    }

    private IEnumerator EsperarAntesDeMorir()
    {
        yield return new WaitForSeconds(3.0f);
        SceneManager.LoadScene("Llegada");
        Destroy(gameObject);
    }
}


