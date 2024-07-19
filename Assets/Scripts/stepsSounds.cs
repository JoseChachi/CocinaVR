using UnityEngine;

public class FootSteps : MonoBehaviour
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
        if (OVR.isGrounded == true && OVR.velocity.magnitude > 2.0f)
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