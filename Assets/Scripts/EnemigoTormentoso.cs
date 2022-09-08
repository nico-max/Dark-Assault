using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemigoTormentoso : MonoBehaviour
{
    public Transform posJugador;
    public float enemySpeed;
    public float range;

    [SerializeField]
    private Animator anim;

    public float counterVida;
    public float vidaMaxima;


    void Start()
    {
        anim = GetComponent<Animator>();
        counterVida = 0;
        vidaMaxima = 1.3f;
        enemySpeed = 12;

        posJugador = GameObject.FindGameObjectWithTag("Player").gameObject.transform;
    }

    void Update()
    {
        ChequearDistancia();
        LookAtPlayerQuat();

        if(counterVida >= vidaMaxima)
        {
            SpawnManager._instance.TormentosoDestruido(this.gameObject);
        }
    }

    void ChequearDistancia()
    {
        if (Vector3.Distance(posJugador.position, transform.position) <= range)
        {
            anim.SetBool("atacar", true);
        }
        else
        {
            anim.SetBool("atacar", false);
            SeguirAlJugador();
        }
    }

    void SeguirAlJugador()
    {
        transform.position = Vector3.MoveTowards(transform.position, posJugador.position, enemySpeed * Time.deltaTime);
    }
    void LookAtPlayerQuat()
    {
        transform.LookAt(posJugador);

    }

    public void Impacto()
    {
        counterVida += Time.deltaTime;
    }
}
