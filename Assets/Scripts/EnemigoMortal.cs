using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemigoMortal : MonoBehaviour
{
    public GameObject Jugador;
    private Animator anim;
    public float speedMovement;

    int DEFAULT_SPEED = 10;

    void Start()
    {
        Jugador = GameObject.FindGameObjectWithTag("Player");
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(Jugador.transform);

        //modo seguimiento 1
        transform.position = Vector3.MoveTowards(transform.position, Jugador.transform.position, speedMovement * Time.deltaTime);

        DeteccionMovimiento();
    }

    void DeteccionMovimiento()
    {
        if(anim.GetCurrentAnimatorStateInfo(0).IsName("stun"))
        {
            speedMovement = 0;
        }
        else
        {
            speedMovement = DEFAULT_SPEED;
        }
    }

    public void ImpactoFlash()
    {
        anim.SetTrigger("stun");
    }
}
