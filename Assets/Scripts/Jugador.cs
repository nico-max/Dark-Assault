using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jugador : MonoBehaviour
{
    //Player Movement
    public float velocidadMovimiento = 30f;

    private Vector3 spawnpoint;

    public bool insideLight;

    void Start()
    {
        spawnpoint = transform.position;
        insideLight = false;
    }

    // Update is called once per frame
    void Update()
    {
        float hor = Input.GetAxis("Horizontal");
        float ver = Input.GetAxis("Vertical");

        transform.Translate(new Vector3(hor, 0, ver) * velocidadMovimiento * Time.deltaTime);

        if(Input.GetKeyDown(KeyCode.Space))
        {
            Respawn();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag=="Checkpoint")
        {
            Transform col = collision.transform;

            spawnpoint = new Vector3(col.position.x, transform.position.y, col.position.z);
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
}
