using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformerMovement : MonoBehaviour
{
    [SerializeField] List<Transform> wayPoints;
    [SerializeField] float speed = 20;
    [SerializeField] float distanciaCambio = 0.2f;
    [SerializeField] float espacio = 0.2f;
    byte siguientePosicion = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (wayPoints != null && wayPoints.Count > 0)
        {
            transform.position = wayPoints[0].position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, wayPoints[siguientePosicion].transform.position, speed * Time.deltaTime);
        if (Vector3.Distance(transform.position, wayPoints[siguientePosicion].transform.position) < distanciaCambio)
        {
            siguientePosicion++;
            if (siguientePosicion >= wayPoints.Count)
            {
                siguientePosicion = 0;
            }
        }
        Alinear();
    }
    void Alinear()
    {
        int i = 0;
        foreach (Transform hijo in transform)
        {
            Vector3 pos = hijo.position;
            pos.x = transform.position.x + (i * espacio);
            pos.y = transform.position.y; 
            hijo.position = pos;
            i++;
        }
    }
}
