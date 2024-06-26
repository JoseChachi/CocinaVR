using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CookingDetector : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter(Collider other)
    {
        GameObject o = other.GetComponent<GameObject>();
        if (o != null)
        {
            o.GetComponent<Renderer>().material.SetColor("Red", Color.red);
            Debug.Log("Entro...");
        }
        else
        {
            Debug.Log("No Entro :( ");
        }
    }

    private void OnTriggerStay(Collider other)
    {
        GameObject o = other.GetComponent<GameObject>();
        if (o != null) {
            o.GetComponent<Renderer>().material.SetColor("Red", Color.red);
            Debug.Log("Entro...");
        } else
        {
            Debug.Log("No Entro :( ");
        }

    }
}
