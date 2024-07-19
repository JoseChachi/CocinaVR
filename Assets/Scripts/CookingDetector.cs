using Oculus.Interaction;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CookingDetector : MonoBehaviour
{
    [SerializeField]
    public float darkenFactor = 0.01f;

    [SerializeField]
    public float darkenSpeed = 5f;

    [SerializeField]
    public AudioSource sound;

    private Renderer rend = null;

    private bool isColliding = false; // Flag to track collision state

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DarkenPeriodicallyCoroutine());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator DarkenPeriodicallyCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(darkenSpeed); // Wait for 10 seconds

            if (isColliding)
            {
                
                Material material = new Material(rend.material);

                material.color = material.color * (1 - darkenFactor);

                rend.material = material;
                
                yield return null;
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Ingredient")
        {
            sound.Play(0);
            rend = collision.gameObject.GetComponent<Renderer>();
            isColliding = true;
            Debug.Log("Object started colliding with DarkeningObject.");
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.collider.tag == "Ingredient")
        {
            sound.Pause();
            rend = null;
            isColliding = false;
            Debug.Log("Object stopped colliding with DarkeningObject.");
        }
    }

    void OnCollisionStay(Collision collision)
    {

        

    }

}
