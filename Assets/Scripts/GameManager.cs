using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager _instance;

    private int level;

    public GameObject[] faros;

    public GameObject faroActivo;

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
        _instance.level = 0;
        //_instance.faros = new GameObject[3];
        _instance.faroActivo = faros[level];
    }


    public void superoNivel()
    {
        level++;
        //Debug.Log("Faro activo a apagar: " + faroActivo.name);
        faroActivo.GetComponent<Faro>().Apagar();
        faroActivo = faros[level];
        //Debug.Log("Faro activo a encender: " + faroActivo.name);
        faroActivo.GetComponent<Faro>().Encender();
    }
}
