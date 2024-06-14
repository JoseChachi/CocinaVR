using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class setOnFloor : MonoBehaviour
{

    public Transform target;
    void Start()
    {
        
    }

    void FixedUpdate()
    {
        transform.position = target.position;
    }
}
