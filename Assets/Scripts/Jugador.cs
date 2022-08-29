using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jugador : MonoBehaviour
{
    //Player Movement
    public float velocidadMovimiento = 30f;
    public float rangoRayo = 100f;
    public float sensibilidadMouse = 140f;

    public float xRotacion = 0f;
    public float yRotacion = 0f;

    public float fuerzaSalto;

    private Vector3 spawnpoint;
    private Vector3 posInicial;
    [SerializeField]
    private Transform enemigoMortal;

    private Rigidbody rig;
    private Transform cam;
    private AudioSource heart;
    public AudioSource foots;

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
    }

    void Update()
    {
        MouseLook();

        if (Input.GetKeyDown(KeyCode.Q))
        {
            Respawn();
        }
        
        comprobarAltura();

        Jump();
        Latidos();

        if(Input.GetKeyDown(KeyCode.K))
        {
            GameManager._instance.activarMortal();
        }
    }

    private void FixedUpdate()
    {
        Move();
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
            insideLight = false;
            GameManager._instance.reiniciarNivel();
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


    void Respawn()
    {
        transform.position = spawnpoint;
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

}
