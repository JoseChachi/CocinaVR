using UnityEngine;

public class WaterFlowController : MonoBehaviour
{
    private ParticleSystem waterParticleSystem;

    void Start()
    {
        waterParticleSystem = GetComponent<ParticleSystem>();
        waterParticleSystem.Stop();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (waterParticleSystem.isPlaying)
            {
                waterParticleSystem.Stop();
            }
            else
            {
                waterParticleSystem.Play();
            }
        }
    }
}
