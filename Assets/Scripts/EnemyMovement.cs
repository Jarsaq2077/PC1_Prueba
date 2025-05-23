using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] List<Transform> wayPoints;
    [SerializeField] float speed = 20;
    [SerializeField] float distanciaCambio = 0.2f;
    [SerializeField] float offsetAlSuelo = 0.5f;
    byte siguientePosicion = 0;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, wayPoints[siguientePosicion].transform.position, speed * Time.deltaTime);
        if (siguientePosicion == 1)
        {
            transform.localScale = new Vector3(-1.0f, 1.0f, 10f);
        }else
        {
            transform.localScale = new Vector3(1.0f, 1.0f, 10f);
        }
        if (Vector3.Distance(transform.position, wayPoints[siguientePosicion].transform.position) < distanciaCambio){
            siguientePosicion++;
            if(siguientePosicion >= wayPoints.Count)
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
}
