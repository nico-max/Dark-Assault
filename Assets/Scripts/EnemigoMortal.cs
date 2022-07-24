using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemigoMortal : MonoBehaviour
{
    public GameObject Jugador;
    public float speedMovement;

    void Start()
    {
        Jugador = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(Jugador.transform);

        //modo seguimiento 1
        transform.position = Vector3.MoveTowards(transform.position, Jugador.transform.position, speedMovement * Time.deltaTime);
    }
}
