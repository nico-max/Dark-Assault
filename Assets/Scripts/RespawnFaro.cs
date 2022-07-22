using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnFaro : MonoBehaviour
{

    private float counter;
    float tiempoInicio = .5f;
    bool iniciado;

    void Start()
    {
        counter = 0f;
        iniciado = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(counter>=tiempoInicio && !iniciado)
        {
            GetComponentInParent<Faro>().iniciarNivel();
            iniciado = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Player" && counter < tiempoInicio)
        {
            counter += Time.deltaTime;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            GetComponentInParent<Faro>().Respawn();
            iniciado = false;
            counter = 0;
        }
    }
}
