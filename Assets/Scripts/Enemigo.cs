using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemigo : MonoBehaviour
{
    public Transform posJugador;
    public float enemySpeed = 1;
    public float range;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {


        ChequearDistancia();
        LookAtPlayerQuat();
    }
    void ChequearDistancia()
    {
        if (Vector3.Distance(posJugador.position, transform.position) <= range)
        { SeguirAlJugador(); }
    }

    void SeguirAlJugador()
    {
        transform.position = Vector3.MoveTowards(transform.position, posJugador.position, enemySpeed * Time.deltaTime);
    }
    void LookAtPlayerQuat()
    {
        Quaternion rot = Quaternion.LookRotation(posJugador.position - transform.position);
        transform.rotation = rot;

    }
}
