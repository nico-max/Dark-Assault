using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Linterna : MonoBehaviour
{
    private Light luz;
    private Camera playerCam;
    private Animator anim;
    private AudioSource sound;

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
        lightRange = luz.range;
        encendida = false;
        bateria = 100;

        playerCam = GetComponentInParent<Camera>();
        anim = GetComponent<Animator>();
        sound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        verificarBateria();
        descargar();
        cargar();

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

        ControlLuzLinterna();

        deteccionLuzLinterna();

        Debug.DrawRay(playerCam.transform.position, playerCam.transform.forward * (lightRange/4), Color.green);
    }

    void deteccionLuzLinterna()
    {
        if(encendida && bateria>0)
        {
            RaycastHit hit;

            if(Physics.Raycast(playerCam.transform.position, playerCam.transform.forward, out hit, lightRange/3, 1))
            {
                if(hit.transform.gameObject.tag == "EnemigoAtormentador")
                {
                    hit.transform.gameObject.GetComponent<EnemigoTormentoso>().Impacto();
                }
            }
        }
    }

    void ControlLuzLinterna()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            luz.enabled = !luz.enabled;
            encendida = !encendida;
        }
        else if (encendida && bateria>10 && Input.GetKeyDown(KeyCode.Mouse1))
        {
            activarAnimator();
            anim.SetTrigger("flash");
            detectarFlash();
            sound.Play();
            bateria -= 10;
            Invoke("activarAnimator", .35f);
        }
    }

    void detectarFlash()
    {
        RaycastHit hit;

        if (Physics.Raycast(playerCam.transform.position, playerCam.transform.forward, out hit, lightRange/2, 1))
        {
            if (hit.transform.gameObject.tag == "EnemigoMortal")
            {
                hit.transform.gameObject.GetComponent<EnemigoMortal>().ImpactoFlash();
            }
            else if (hit.transform.gameObject.tag == "EnemigoAtormentador")
            {
                SpawnManager._instance.TormentosoDestruido(hit.transform.gameObject);
            }
        }

    }

    void activarAnimator()
    {
        anim.enabled = !anim.enabled;
    }

    void cargar()
    {
        if(!encendida && bateria<100)
        {
            bateria += Time.deltaTime * 2;
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

    public void setBattery(float _bat)
    {
        this.bateria = _bat;
    }

    public void recarcar()
    {
        this.bateria = 100;
    }
}
