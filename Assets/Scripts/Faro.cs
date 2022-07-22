using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Faro : MonoBehaviour
{
    private Light luzFaro;
    public Transform deteccion;

    public bool reducir;

    public float reductionSpeed;

    private Vector3 inicio;
    private Animator anim;


    void Start()
    {
        luzFaro = GetComponentInChildren<Light>();
        reductionSpeed = 1;

        inicio = transform.position;

        anim = GetComponent<Animator>();

    }
    
    void Update()
    {
        if(reducir)
        {
            reducirHalo();
        }
        
    }

    void reducirHalo()
    {
        float halo = luzFaro.spotAngle;

        halo -= Time.deltaTime * reductionSpeed;

        float nuevoRadio = (halo * 70) / 100;

        deteccion.localScale = new Vector3(nuevoRadio, 6, nuevoRadio);
        luzFaro.spotAngle = halo;
    }

    public void Respawn()
    {
        transform.position = inicio;
        anim.SetBool("iniciar", false);
    }

    public void iniciarNivel()
    {
        anim.SetBool("iniciar", true);
    }

}
