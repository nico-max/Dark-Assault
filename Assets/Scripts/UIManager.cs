using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager _instance;

    // Elementos de interfaz
    public Image _cuadroLinterna;


    // Sprites 
    public Sprite lintMuerta;
    public Sprite lintBaja;
    public Sprite lintMidVacia;
    public Sprite lintMidLlena;
    public Sprite lintLlena;


    // Variables para funciones varias
    [SerializeField]
    bool bateriaBaja;
    float counter;
    float TIEMPO_PARPADEO;
    float TIEMPO_PARPADEO_NORMAL = .5f;
    float TIEMPO_PARPADEO_URGENTE = .2f;


    public GameObject pauseMenu;
    public GameObject menuWin;
    public GameObject menuDead;

    public Toggle toggleSight;
    public Image sight;


    void Awake()
    {
        if (UIManager._instance == null)
        {
            UIManager._instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        bateriaBaja = false;
        counter = 0;
        TIEMPO_PARPADEO = TIEMPO_PARPADEO_NORMAL;
    }

    // Update is called once per frame
    void Update()
    {
        parpadeoLinterna();
    }

    public void actualizarBateriaLinterna(float bateria)
    {
        if (bateria > 75)
        {
            _cuadroLinterna.sprite = lintLlena;
        }
        else if (bateria > 50)
        {
            _cuadroLinterna.sprite = lintMidLlena;
        }
        else if (bateria > 25)
        {
            _cuadroLinterna.sprite = lintMidVacia;
            bateriaBaja = false;
        }
        else if(bateria > 0)
        {
            _cuadroLinterna.sprite = lintBaja;
            bateriaBaja = true;
        }
        else
        {
            _cuadroLinterna.sprite = lintMuerta;
            bateriaBaja = false;
        }

        if (bateria > 10)
        {
            TIEMPO_PARPADEO = TIEMPO_PARPADEO_NORMAL;
        }
        else
        {
            TIEMPO_PARPADEO = TIEMPO_PARPADEO_URGENTE;
        }
    }

    void parpadeoLinterna()
    {
        if(bateriaBaja)
        {
            if(counter >= 0)
            {
                counter -= Time.deltaTime;
            }
            else
            {
                _cuadroLinterna.enabled = !_cuadroLinterna.IsActive();
                counter = TIEMPO_PARPADEO;
            }
        }
        else
        {
            _cuadroLinterna.enabled = true;
        }
    }

    public void TogglePauseMenu()
    {
        pauseMenu.SetActive(!pauseMenu.activeInHierarchy);
    }

    public void setGano()
    {
        menuWin.SetActive(true);
    }

    public void ToggleSights()
    {
        sight.enabled = toggleSight.isOn;
    }

    public void setMurio()
    {
        menuDead.SetActive(true);
        Cursor.lockState = CursorLockMode.Confined;
    }

    public void unsetMurio()
    {
        menuDead.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
    }
}
