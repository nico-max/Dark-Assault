using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class Jugador : MonoBehaviour
{
    //Player Movement
    public float velocidadMovimiento = 30f;
    public float rangoRayo = 100f;
    public float sensibilidadMouse = 140f;
    float SENSIBILIDAD_DEFAULT = 140f;
    float SENSIBILIDAD_STUN = 50f;

    float counterStun;
    float TIEMPO_STUN = 1f;
    bool stunned;

    public float xRotacion = 0f;
    public float yRotacion = 0f;

    public float fuerzaSalto;

    [SerializeField]
    private Vector3 spawnpoint;
    private Vector3 posInicial;

    // Postprocessing features
    [SerializeField]
    private PostProcessVolume _volume;
    private Vignette _vignette;

    private Transform enemigoMortal;

    private Rigidbody rig;
    private Transform cam;

    // sonidos
    private AudioSource heart;
    public AudioSource foots;

    private Linterna _linterna;

    public bool insideLight;
    public bool tocandoPiso;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        rangoRayo = 100f;

        spawnpoint = transform.position;
        posInicial = transform.position;
        insideLight = false;
        rig = GetComponent<Rigidbody>();
        cam = GetComponentInChildren<Camera>().transform;
        heart = GetComponent<AudioSource>();

        enemigoMortal = GameObject.FindGameObjectWithTag("EnemigoMortal").transform;
        _linterna = GetComponentInChildren<Linterna>();
        //_volume = GameObject.FindGameObjectWithTag("GlobalPostProcessing").GetComponent<PostProcessVolume>();

        _volume.profile.TryGetSettings(out _vignette);
        configDefault();
    }

    void Update()
    {
        MouseLook();

        if (Input.GetKeyDown(KeyCode.Q))
        {
            Respawn();
        }
        
        comprobarAltura();
        deteccionStun();

        Jump();
        Latidos();

        if(Input.GetKeyDown(KeyCode.K))
        {
            GameManager._instance.activarMortal();
        }
    }

    private void FixedUpdate()
    {
        if(!stunned)
        {
            Move();
        }
    }

    void Move()
    {
        float hor = Input.GetAxisRaw("Horizontal");
        float ver = Input.GetAxisRaw("Vertical");

        //Debug.Log("hor: " + hor);
        //Debug.Log("ver: " + ver);

        Vector3 velocity = Vector3.zero;
        Vector3 inputPlayer = (transform.forward * ver + transform.right * hor).normalized;
        //Debug.Log(inputPlayer);

        if(inputPlayer != Vector3.zero)
        {
            velocity = inputPlayer * velocidadMovimiento;
            foots.UnPause();
        }
        else if(inputPlayer == Vector3.zero)
        {
            foots.Pause();
        }

        velocity.y = rig.velocity.y;

        rig.velocity = velocity;
    }

    void MouseLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensibilidadMouse * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensibilidadMouse * Time.deltaTime;

        xRotacion -= mouseY;
        xRotacion = Mathf.Clamp(xRotacion, -80, 80);

        yRotacion += mouseX;//esto es asi sino funciona al reves
        cam.localRotation = Quaternion.Euler(xRotacion, 0, 0);

        transform.Rotate(Vector3.up * mouseX);

    }

    void Jump()
    {
        if(Input.GetKeyDown(KeyCode.Space) && tocandoPiso)
        {
            rig.AddForce(Vector3.up * fuerzaSalto, ForceMode.Impulse);
        }
    }

    void comprobarAltura()
    {
        RaycastHit hit;

        if(Physics.Raycast(transform.position, Vector3.down, out hit, rangoRayo))
        {
            if(hit.distance <= 2.5f)
            {
                tocandoPiso = true;
            }
            else
            {
                tocandoPiso = false;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag=="Checkpoint")
        {
            Transform col = collision.transform;

            spawnpoint = new Vector3(col.position.x, transform.position.y, col.position.z);
        }

        if (collision.gameObject.tag == "EnemigoMortal")
        {
            insideLight = false;
            spawnpoint = posInicial;
            GameManager._instance.reiniciarNivel();
        }
        else if(collision.gameObject.tag == "EnemigoAtormentador")
        {
            Destroy(collision.gameObject);
            reiniciarCounterStun();
            configStun();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "HaloLuz")
        {
            insideLight = true;
        }
        else if(other.gameObject.tag == "EntradaCheckpoint")
        {
            insideLight = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "HaloLuz" && insideLight)
        {
            Respawn();
        }
    }


    public void Respawn()
    {
        transform.position = spawnpoint;
        _linterna.recarcar();
        SpawnManager._instance.destroyTormentosos();
        SpawnManager._instance.detenerNivel();
        //GameManager._instance.reiniciarMortal();
    }

    void Latidos()
    {
        
        float distancia = (enemigoMortal.position - transform.position).magnitude;

        //Debug.Log(hit.transform.gameObject.name);
        //Debug.Log(distancia);

        if(enemigoMortal.gameObject.activeInHierarchy)
        {
            // Configuraciones dependiendo de la distancia del enemigo mortal
            if (distancia > 200f)
            {
                heart.pitch = 0.75f;
                heart.volume = 0.3f;
            }
            else if (distancia >= 150f)
            {
                heart.pitch = 1;
                heart.volume = 0.7f;
            }
            else if (distancia >= 100f)
            {
                heart.pitch = 1.25f;
                heart.volume = 0.7f;
            }
            else if (distancia >= 50f)
            {
                heart.pitch = 1.5f;
                heart.volume = 0.7f;
            }
            else if (distancia >= 25f)
            {
                heart.pitch = 1.75f;
                heart.volume = 0.7f;
            }
            else
            {
                heart.pitch = 2;
                heart.volume = 0.75f;
            }
        }
        else
        {
            // Configuracion de volumen y velocidad default
            heart.pitch = 0.75f;
            heart.volume = 0.3f;
        }
        
        
    }

    public void setSpawnpoint(Vector3 newSpawn)
    {
        this.spawnpoint = newSpawn;
    }

    void reiniciarCounterStun()
    {
        counterStun = TIEMPO_STUN;
    }

    void deteccionStun()
    {
        if(counterStun > 0)
        {
            counterStun -= Time.deltaTime;
        }
        else
        {
            configDefault();
        }
    }

    void configStun()
    {
        sensibilidadMouse = SENSIBILIDAD_STUN;
        _vignette.active = true;
        stunned = true;
        rig.velocity = Vector3.zero;
    }

    void configDefault()
    {
        sensibilidadMouse = SENSIBILIDAD_DEFAULT;
        _vignette.active = false;
        stunned = false;
    }
}
