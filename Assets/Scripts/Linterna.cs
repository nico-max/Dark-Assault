using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Linterna : MonoBehaviour
{
    private Light luz;

    public float bateria;
    public bool encendida;

    enum lightBehavior
    {
        bateriaAlta,
        bateriaMedia,
        bateriaBaja,
        bateriaMuerta
    }

    private lightBehavior comportamientoLuz;

    private float lightRange;

    private float counter;

    // Start is called before the first frame update
    void Start()
    {
        luz = GetComponent<Light>();
        luz.enabled = false; 
        encendida = false;
        bateria = 100;
    }

    // Update is called once per frame
    void Update()
    {
        verificarBateria();
        descargar();

        switch (comportamientoLuz)
        {
            case lightBehavior.bateriaAlta:
                configHighBattery();
                break;

            case lightBehavior.bateriaMedia:
                configMidBattery();
                break;

            case lightBehavior.bateriaBaja:
                configLowBattery();
                break;

            case lightBehavior.bateriaMuerta:
                configDeadBattery();
                break;
        }

        if(Input.GetKeyDown(KeyCode.F))
        {
            luz.enabled = !luz.enabled;
            encendida = !encendida;
        }
    }

    void cargar()
    {
        bateria += 50f;

        if(bateria>100)
        {
            bateria = 100;
        }
    }

    void descargar()
    {
        if(encendida && bateria>0)
        {
            bateria -= Time.deltaTime;
        }
    }

    void configHighBattery()
    {
        luz.range = 100;
    }

    void configMidBattery()
    {
        luz.range = 80;
    }

    void configLowBattery()
    {
        if(counter>0)
        {
            counter -= Time.deltaTime;
        }
        else
        {
            reiniciarCounter();
            float num = Random.Range(0, 100);

            if(num>40)
            {
                luz.range = 80;
            }
            else
            {
                luz.range = 0;
            }
        }

    }

    void configDeadBattery()
    {
        luz.range = 0;
    }

    void verificarBateria()
    {
        if(bateria>50)
        {
            comportamientoLuz = lightBehavior.bateriaAlta;
        }
        else if(bateria>10)
        {
            comportamientoLuz = lightBehavior.bateriaMedia;
        }
        else if(bateria>0)
        {
            comportamientoLuz = lightBehavior.bateriaBaja;
        }
        else
        {
            comportamientoLuz = lightBehavior.bateriaMuerta;
        }
    }

    void reiniciarCounter()
    {
        counter = .1f;
    }
}
