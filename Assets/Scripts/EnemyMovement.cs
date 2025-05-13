using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] List<Transform> wayPoints;
    [SerializeField] float speed = 20;
    [SerializeField] float distanciaCambio = 0.2f;
    byte siguientePosicion = 0;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, wayPoints[siguientePosicion].transform.position, speed * Time.deltaTime);
        if (Vector3.Distance(transform.position, wayPoints[siguientePosicion].transform.position) < distanciaCambio){
            siguientePosicion++;
            if(siguientePosicion >= wayPoints.Count)
            {
                siguientePosicion = 0;
            }
        }
    }
}
