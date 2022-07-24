using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnFaro : MonoBehaviour
{

    private float counter;
    float tiempoInicio = .5f;
    bool iniciado;

    [SerializeField]
    private Faro faroPadre;

    void Start()
    {
        counter = 0f;
        iniciado = false;

        faroPadre = GetComponentInParent<Faro>();
    }

    // Update is called once per frame
    void Update()
    {
        if(counter>=tiempoInicio && !iniciado)
        {
            faroPadre.iniciarNivel();
            iniciado = true;
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "EnemigoMortal")
        {
            faroPadre.AlertaEnemigo();
        }

        if (other.gameObject.tag == "Player" && counter < tiempoInicio)
        {
            counter += Time.deltaTime;
        }
        //Debug.Log(other.gameObject.tag);
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            faroPadre.Respawn();
            iniciado = false;
            counter = 0;
        }
        
        if(other.gameObject.tag == "EnemigoMortal")
        {
            faroPadre.EnemigoSalio();
        }
    }
}
