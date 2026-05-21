using UnityEngine;

public class ParticleThingo : MonoBehaviour
{
    [SerializeField] private ParticleSystem particle;

    public void SpawnParticle()
    {
        Instantiate(
            particle,
            transform.position,
            Quaternion.identity
        );
    }
}
