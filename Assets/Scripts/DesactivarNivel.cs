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

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            GetComponent<BoxCollider>().isTrigger = false;

            GameManager._instance.superoNivel();
        }
    }
}
