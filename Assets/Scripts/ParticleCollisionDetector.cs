using UnityEngine;

public class ParticleCollisionDetector : MonoBehaviour
{
    public Transform waterSurface; // El objeto que representa la superficie del agua
    private int particleCount = 0;
    public float heightIncrement = 0.01f; // Incremento de altura por partícula

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("WaterParticle"))
        {
            particleCount++;
            AdjustWaterLevel();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("WaterParticle"))
        {
            particleCount--;
            AdjustWaterLevel();
        }
    }

    void AdjustWaterLevel()
    {
        waterSurface.position = new Vector3(waterSurface.position.x, particleCount * heightIncrement, waterSurface.position.z);
    }
}