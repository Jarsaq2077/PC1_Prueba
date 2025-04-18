using UnityEngine;
using UnityEngine.InputSystem;

public class GenVillalobos : MonoBehaviour
{
    public float Speed;
    public float JumpForce;
    public int vidas;
    private bool EnSuelo;
    //private Animator Animator;
    private Rigidbody2D Rigidbody2D;
    private float Horizontal;
    public float moveSpeed;
    private Vector2 direction;
    Vector2 startPosition;

    public LayerMask capaSuelo;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Horizontal = Input.GetAxisRaw("Horizontal");

        //Animator.SetBool("Corriendo", Horizontal != 0.0f);

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
    }

    private void Jump()
    {
        Rigidbody2D.AddForce(Vector2.up*JumpForce);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ZonaMuerta"))
        { 
           perderVida(); 
        }           
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Meta"))
        {
            ResetLevel();
        }
    }
    private void perderVida()
    {
        vidas = vidas - 1;
        if (vidas <= 0)
        {
            ResetLevel();
        }
    }
    private void FixedUpdate()
    {
        Rigidbody2D.linearVelocity = new Vector2(Horizontal, Rigidbody2D.linearVelocity.y);
    }
    private void ResetLevel()
    {
        transform.position = startPosition;
        Rigidbody2D.linearVelocity = Vector2.zero;
    }
}
