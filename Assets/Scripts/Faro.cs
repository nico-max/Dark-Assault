using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Faro : MonoBehaviour
{
    private Light luzFaro;
    public Transform deteccion;

    public bool reducir;

    public float reductionSpeed;


    void Start()
    {
        luzFaro = GetComponentInChildren<Light>();
        reductionSpeed = 1;
        
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

}
