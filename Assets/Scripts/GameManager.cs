using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager _instance;

    private int level;

    public GameObject[] faros;

    public GameObject faroActivo;

    private Transform posJugador;
    private Transform posEnemigoMortal;

    [SerializeField]
    private Vector3 posInicialJugador;
    private Vector3 posInicialEnemigoMortal;

    void Awake()
    {
        if(GameManager._instance == null)
        {
            GameManager._instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        level = 0;
        //_instance.faros = new GameObject[3];
        faroActivo = faros[level];

        posJugador = GameObject.FindGameObjectWithTag("Player").transform;
        posEnemigoMortal = GameObject.FindGameObjectWithTag("EnemigoMortal").transform;

        posInicialJugador = _instance.posJugador.position;
        posInicialEnemigoMortal = _instance.posEnemigoMortal.position;

        posEnemigoMortal.gameObject.SetActive(false);
    }


    public void superoNivel()
    {
        level++;
        //Debug.Log("Faro activo a apagar: " + faroActivo.name);
        faroActivo.GetComponent<Faro>().Apagar();
        faroActivo = faros[level];
        //Debug.Log("Faro activo a encender: " + faroActivo.name);
        faroActivo.GetComponent<Faro>().Encender();

        if(level==2)
        {
            activarMortal();
        }
    }

    public void activarMortal()
    {
        posEnemigoMortal.gameObject.SetActive(true);
    }

    public void reiniciarNivel()
    {
        posJugador.position = posInicialJugador;
        posEnemigoMortal.position = posInicialEnemigoMortal;

        foreach(GameObject faro in faros)
        {
            faro.GetComponent<Faro>().Respawn();
            faro.GetComponent<Faro>().Apagar();
        }

        level = 0;
        faroActivo = faros[level];
        faroActivo.GetComponent<Faro>().Encender();

        posEnemigoMortal.gameObject.SetActive(false);

    }
}
