using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager _instance;

    [SerializeField]
    private int level;

    public GameObject[] faros;

    public GameObject faroActivo;

    private Transform posJugador;
    private Transform posEnemigoMortal;

    [SerializeField]
    private Vector3 posInicialJugador;
    private Vector3 posInicialEnemigoMortal;

    private int escena;

    bool pauseActive;
    bool completoEscena;

    void Awake()
    {
        if(GameManager._instance == null)
        {
            DontDestroyOnLoad(gameObject);
            GameManager._instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        cargarEscena();
    }

    private void Start()
    {
        escena = 1;
        completoEscena = false;
    }

    private void Update()
    {
        TogglePause();

        if(completoEscena)
        {
            cargarEscena();
            SpawnManager._instance.cargarNivel();
            completoEscena = false;
        }

        levelSuccessCheck();

    }

    void cargarEscena()
    {
        pauseActive = false;

        level = 0;
        _instance.faros = GameObject.FindGameObjectsWithTag("FaroLuz");
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
        faroActivo.GetComponent<Faro>().Apagar();
        faroActivo = faros[level];
        faroActivo.GetComponent<Faro>().Encender();

        SpawnManager._instance.cargarNivel();
    }

    public void activarMortal()
    {
        posEnemigoMortal.gameObject.SetActive(true);
    }

    public void reiniciarMortal()
    {
        posEnemigoMortal.position = posInicialEnemigoMortal;
    }

    public void reiniciarNivel()
    {
        posJugador.gameObject.GetComponent<Jugador>().setSpawnpoint(posInicialJugador);
        posJugador.gameObject.GetComponent<Jugador>().Respawn();
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

        SpawnManager._instance.cargarNivel();
    }

    public int getLevel()
    {
        return this.level;
    }

    private void TogglePause()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(pauseActive)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    void PauseGame()
    {
        UIManager._instance.TogglePauseMenu();
        pauseActive = true;
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.Confined;
    }

    public void ResumeGame()
    {
        UIManager._instance.TogglePauseMenu();
        pauseActive = false;
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public bool IsPauseActive()
    {
        return pauseActive;
    }

    void Gano()
    {
        UIManager._instance.setGano();
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.Confined;
    }

    void levelSuccessCheck()
    {
        if(posJugador.position.y <= -10)
        {
            level = 0;
            escena++;
            completoEscena = true;

            if(escena < 3)
            {
                SceneManager.LoadScene(escena);
            }
            else if(escena >= 3)
            {
                posJugador.position.Set(posJugador.position.x, 20, posJugador.position.z);
                Gano();
            }
            
        }
    }
}
