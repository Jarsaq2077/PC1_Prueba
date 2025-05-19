using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using System.Collections;


public class GenVillalobos : MonoBehaviour
{
    public float Speed;
    public float JumpForce;
    public int vidas;
    private bool EnSuelo;
    private Animator Animator;
    private Rigidbody2D Rigidbody2D;
    private float Horizontal;
    public float moveSpeed;
    private Vector2 direction;
    Vector2 startPosition;

    public LayerMask capaSuelo;

    public AudioClip sonidoSalto;
    public AudioClip sonidoMuerte;
    public AudioClip sonidoFondo;
    private AudioSource audioSource;

    public Image[] corazones;
    public Sprite corazonLleno;
    public Sprite corazonVacio;

    bool esIntangible = false;
    public float tiempoIntangible = 2f;

    private CapsuleCollider2D capsuleCollider;
    private BoxCollider2D boxCollider;

    private GameObject objetoRecogido = null;
    public Transform objetoRecogidoSlot;
    public LayerMask layerObjetoRecogible;

    public bool haskey = false;
    private Vector3 posicionObjInicial;
    [SerializeField] GameObject puerta;
    [SerializeField] GameObject deadEnd;
    [SerializeField] GameObject enemigoSorpresa;
    public bool hasfake = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        Rigidbody2D = GetComponent<Rigidbody2D>();
        startPosition = transform.position;
        Animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        vidas = 3;
        ActualizarVidas();
        if (sonidoFondo != null)
        {
            audioSource.clip = sonidoFondo;
            audioSource.loop = true;
            audioSource.Play();
        }
    }

    // Update is called once per frame
    void Update()
    {
        Horizontal = Input.GetAxisRaw("Horizontal");
        Animator.SetBool("Corriendo", Horizontal != 0.0f);

        if (Input.GetKeyDown(KeyCode.E) && objetoRecogido == null)
        {
            // Lanzamos un Raycast hacia el frente del jugador para detectar el objeto
            Collider2D[] objetos = Physics2D.OverlapCircleAll(transform.position, 0.5f, layerObjetoRecogible);

            foreach (Collider2D col in objetos)
            {
                Debug.Log("Objeto detectado: " + col.name + " | Tag: " + col.tag);
                if (col.CompareTag("objeto") || col.CompareTag("llave") || col.CompareTag("arma") || col.CompareTag("tesoroFalso"))
                {
                    RecogerObjeto(col.gameObject);
                    break;
                }
            }
        }

        // Soltar el objeto si está siendo recogido
        if (Input.GetKeyDown(KeyCode.Q) && objetoRecogido != null)
        {
            SoltarObjeto();
        }

        if (Input.GetKeyDown(KeyCode.F) && objetoRecogido != null && !esIntangible)
        {
            Debug.Log("Golpe con objeto recogido");

            GolpeArma golpe = objetoRecogido.GetComponent<GolpeArma>();
            if (golpe != null)
            {
                golpe.ActivarGolpe();

                Collider2D col = objetoRecogido.GetComponent<Collider2D>();
                if (col != null)
                {
                    col.enabled = true;
                    StartCoroutine(DesactivarColliderTrasGolpe(col));
                }
            }
        }

        if (Horizontal == 1)
        {            
            transform.localScale = new Vector3(1.0f, 1.0f, 10f);
            
            direction = Vector2.right;
        }
        else if (Horizontal== -1)
        {            
            transform.localScale = new Vector3(-1.0f, 1.0f, 10f);
            
            direction = Vector2.left;
        }
        else
        {
            Animator.SetBool("Corriendo", false);
            direction = Vector2.zero;
        }
        float raycastDistance = 0.15f;

        BoxCollider2D box = GetComponent<BoxCollider2D>();
        float halfWidth = 0.09f;
        Vector2 center = (Vector2)transform.position + box.offset;

        Vector2 leftRayOrigin = center + Vector2.left * halfWidth;
        Vector2 rightRayOrigin = center + Vector2.right * halfWidth;

        bool groundedLeft = Physics2D.Raycast(leftRayOrigin, Vector2.down, raycastDistance);
        bool groundedRight = Physics2D.Raycast(rightRayOrigin, Vector2.down, raycastDistance);

        EnSuelo = groundedLeft || groundedRight;

        Debug.DrawRay(leftRayOrigin, Vector2.down * raycastDistance, Color.red);
        Debug.DrawRay(rightRayOrigin, Vector2.down * raycastDistance, Color.red);

        if (Input.GetKeyDown(KeyCode.Space) && EnSuelo)
        {
            Jump();
        }
        if (!EnSuelo)
        {
            Animator.SetBool("Corriendo", true);
        }
    }
    private void RecogerObjeto(GameObject objeto)
    {
        objetoRecogido = objeto;
        if(objeto.CompareTag("llave"))
        {
            haskey = true;
        }
        if (objeto.CompareTag("tesoroFalso"))
        {
            hasfake = true;
        }
        
        Rigidbody2D rb = objetoRecogido.GetComponent<Rigidbody2D>();
        if (rb != null) rb.simulated = false;

        Collider2D col = objetoRecogido.GetComponent<Collider2D>();
        if (col != null) col.isTrigger = true;        

        // Hacer el objeto hijo del jugador
        objetoRecogido.transform.SetParent(objetoRecogidoSlot);
        objetoRecogido.transform.localPosition = Vector3.zero; // Ajustar la posición del objeto al lado del jugador
    }

    public void SoltarObjeto()
    {
        // Soltar el objeto
        objetoRecogido.transform.SetParent(null);

        // Reactivar la física del objeto
        Rigidbody2D rb = objetoRecogido.GetComponent<Rigidbody2D>();
        if (rb != null) rb.simulated = true;

        Collider2D col = objetoRecogido.GetComponent<Collider2D>();
        if (col != null) { col.enabled = true; col.isTrigger = false; }

        haskey = false;
        objetoRecogido = null;
    }
    private IEnumerator DesactivarColliderTrasGolpe(Collider2D col)
    {
        yield return new WaitForSeconds(0.2f);
        if (col != null)
            col.enabled = false;
    }
    private void Jump()
    {
        Rigidbody2D.AddForce(Vector2.up*JumpForce);
        if (sonidoSalto != null) audioSource.PlayOneShot(sonidoSalto);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ZonaMuerta"))
        {
            if (sonidoMuerte != null) audioSource.PlayOneShot(sonidoMuerte);
            PerderVida();

        } else if (collision.gameObject.CompareTag("enemigo"))
        {
            ChoqueEnemigo(collision);
        } else if (collision.gameObject.CompareTag("surprise"))
        {
            BadEnd();
        }
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Meta") )
        {
            Meta();
        }else if (collision.gameObject.CompareTag("fin"))
        {
            fin();
        }
        else if (collision.gameObject.CompareTag("puerta") && haskey)
        {
            Debug.Log("subiendo puerta");
            Destroy(objetoRecogido);
            StartCoroutine(SubirPuerta());
            haskey = false;
        }else if (collision.gameObject.CompareTag("deadEnd") && hasfake)
        {
            Collider2D col = deadEnd.GetComponent<Collider2D>();
            if (col != null) { col.enabled = true; col.isTrigger = false; }
        }else if (collision.gameObject.CompareTag("surprise") && hasfake)
        {
            Debug.Log("Sorpresita");
            StartCoroutine(BajarEnemigo(enemigoSorpresa, this.transform));

        }
    }
    private void ChoqueEnemigo(Collision2D collision)
    {
        if (!esIntangible)
        {
            if (sonidoMuerte != null) audioSource.PlayOneShot(sonidoMuerte);
            vidas--;
            ActualizarVidas();
            if (vidas <= 0)
            {
                GameOver(); // cambia la escena
            }
            else
            {
                StartCoroutine(HacerIntangibleTemporalmente(collision.collider));
            }            
        }
       
    }
    private void PerderVida()
    {
        vidas--;
        ActualizarVidas();
        if (vidas <= 0)
        {
            GameOver(); // cambia la escena
        }
        else
        {
            ResetLevel();
        }
    }
    private void ActualizarVidas()
    {
        for (int i = 0; i < corazones.Length; i++)
        {
            if (i < vidas)
            {
                corazones[i].sprite = corazonLleno;
            }
            else
            {
                corazones[i].sprite = corazonVacio;
            }
        }
    }
    private IEnumerator HacerIntangibleTemporalmente(Collider2D enemigoCollider)
    {
        esIntangible = true;

        GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.5f);

        // Ignorar colisiones entre cada collider del jugador y el del enemigo
        Physics2D.IgnoreCollision(capsuleCollider, enemigoCollider, true);
        Physics2D.IgnoreCollision(boxCollider, enemigoCollider, true);

        yield return new WaitForSeconds(tiempoIntangible);

        // Volver a activar la colisión
        Physics2D.IgnoreCollision(capsuleCollider, enemigoCollider, false);
        Physics2D.IgnoreCollision(boxCollider, enemigoCollider, false);

        GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1f);

        esIntangible = false;
    }
  
    private void FixedUpdate()
    {
        Rigidbody2D.linearVelocity = new Vector2(Horizontal, Rigidbody2D.linearVelocity.y);
    }
  
    private void GameOver()
    {
        SceneManager.LoadScene("GameOver");
    }
    private void BadEnd()
    {
        SceneManager.LoadScene("BadEnd");
    }
    private IEnumerator SubirPuerta()
    {
        float alturaObjetivo = puerta.transform.position.y + 3.0f;
        float velocidad = 0.2f;

        while (puerta.transform.position.y < alturaObjetivo)
        {
            puerta.transform.position += new Vector3(0, velocidad * Time.deltaTime, 0);
            yield return null;
        }
        Vector3 pos = puerta.transform.position;
        puerta.transform.position = new Vector3(pos.x, alturaObjetivo, pos.z);
    }
    private IEnumerator BajarEnemigo(GameObject enemigoSorpresa, Transform jugador)
    {
        //float alturaInicio = 1.0f; // Altura desde donde cae el enemigo
        float velocidadCaida = 0.5f;

        Vector3 destino = new Vector3(jugador.position.x, jugador.position.y, enemigoSorpresa.transform.position.z);

        
        while (enemigoSorpresa.transform.position.y > destino.y)
        {
            enemigoSorpresa.transform.position -= new Vector3(0, velocidadCaida * Time.deltaTime, 0);
            yield return null;
        }

        
    }
    private void Meta()
    {
        SceneManager.LoadScene("Nivel 2");
    }
    private void fin()
    {
        SceneManager.LoadScene("Llegada");
    }
    private void ResetLevel()
    {
        transform.position = startPosition;
        Rigidbody2D.linearVelocity = Vector2.zero;
        
    }

}
