using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesactivarNivel : MonoBehaviour
{
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "EnemigoMortal")
        {
            GetComponent<BoxCollider>().isTrigger = true;
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            GetComponent<BoxCollider>().isTrigger = false;

            GameManager._instance.superoNivel();
        }

        if(other.gameObject.tag == "EnemigoMortal")
        {
            GetComponent<BoxCollider>().isTrigger = false;
        }
    }
}
