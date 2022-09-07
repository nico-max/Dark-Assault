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

    public GameObject _prefabMonter1;
    public GameObject _prefabMonter2;

    float spawnCounter;

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

    void Start()
    {
        spawnear = false;
        cargarNivel();
    }

    // Update is called once per frame
    void Update()
    {
        enemyLevelSpawn();
    }

    public void cargarNivel()
    {
        int level = GameManager._instance.getLevel();
        GameObject[] groups_spawnpoints = GameObject.FindGameObjectsWithTag("EnemySpawnpoints_nivel");

        group_spawnPoints = groups_spawnpoints[level];

        int childs = group_spawnPoints.transform.childCount;

        for(int i = 0; i < childs; i++)
        {
            spawnPositions.Add(group_spawnPoints.transform.GetChild(i).position);
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
        if(spawnear)
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
                        Instantiate(monster, position, Quaternion.identity);
                    }
                }
                spawnCounter = 5;
            }
        }
    }

    public void destroyTormentosos()
    {
        foreach(GameObject tormentoso in GameObject.FindGameObjectsWithTag("EnemigoAtormentador"))
        {
            Destroy(tormentoso);
        }
    }
}
