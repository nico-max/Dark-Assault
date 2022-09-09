using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager _instance;

    [SerializeField]
    GameObject group_spawnPoints;
    [SerializeField]
    List<Vector3> spawnPositions;
    List<GameObject> spawnedMonsters;

    public GameObject _prefabMonter1;
    public GameObject _prefabMonter2;

    [SerializeField]
    float spawnCounter;

    [SerializeField]
    bool spawnear;


    void Awake()
    {
        if (SpawnManager._instance == null)
        {
            SpawnManager._instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

    }

    private void Start()
    {
        cargarNivel();
    }

    // Update is called once per frame
    void Update()
    {
        enemyLevelSpawn();
    }

    public void cargarNivel()
    {
        // Carga de puntos de spawn para los tormentosos

        spawnear = false;
        spawnedMonsters = new List<GameObject>();

        int level = GameManager._instance.getLevel();

        GameObject[] groups_spawnpoints = GameObject.FindGameObjectsWithTag("EnemySpawnpoints_nivel");


        group_spawnPoints = groups_spawnpoints[level];

        spawnPositions = new List<Vector3>();

        int childs = group_spawnPoints.transform.childCount;

        if(childs > 0)
        {
            for (int i = 0; i < childs; i++)
            {
                spawnPositions.Add(group_spawnPoints.transform.GetChild(i).position);
            }
        }
        else
        {
            detenerNivel();
        }

        // Carga de spawn del mortal

        GameObject[] mortal_spawnpoints = GameObject.FindGameObjectsWithTag("MortalSpawnpoint");

        GameObject mortalSpawnpoint = mortal_spawnpoints[level];

        int child = mortalSpawnpoint.transform.childCount;


        if(child > 0)
        {
            GameManager._instance.activarMortal();
        }

    }

    public void iniciarNivel()
    {
        spawnear = true;
    }

    public void detenerNivel()
    {
        spawnear = false;
    }

    void enemyLevelSpawn()
    {
        if(spawnear && spawnedMonsters.Count <= 20)
        {
            if (spawnCounter > 0)
            {
                spawnCounter -= Time.deltaTime;
            }
            else
            {
                foreach (Vector3 position in spawnPositions)
                {
                    int probability = Random.Range(0, 100);
                    int monsterProbability = Random.Range(0, 100);
                    GameObject monster = monsterProbability > 50 ? _prefabMonter1 : _prefabMonter2;

                    if (probability < 25)
                    {
                        spawnedMonsters.Add(Instantiate(monster, position, Quaternion.identity));
                    }
                }
                spawnCounter = 5;
            }
        }
    }

    public void destroyTormentosos()
    {
        spawnedMonsters.Clear();
        foreach (GameObject tormentoso in GameObject.FindGameObjectsWithTag("EnemigoAtormentador"))
        {
            Destroy(tormentoso);
        }
    }

    public void TormentosoDestruido(GameObject destroyed)
    {
        spawnedMonsters.Remove(destroyed);
        Destroy(destroyed);
    }
}
