using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class STEPSSOUND : MonoBehaviour
{
    float Timer = 0.0f;
    CharacterController OVR;
    AudioSource audioData;

    // Use this for initialization
    void Start()
    {
        OVR = GetComponent<CharacterController>();
        audioData = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (OVR.isGrounded == true && OVR.velocity.magnitude > 1.2f)
        {
            if (Timer > 0.3f)
            {
                audioData.Play(0);
                Timer = 0.0f;
            }

            Timer += Time.deltaTime;
        }
    }
}


