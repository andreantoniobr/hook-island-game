using UnityEngine;

public class PlayerParticlesController : MonoBehaviour
{
    [SerializeField] private ParticleSystem playerParticleDust;

    private void Awake()
    {
        PlayerController.OnTarget += OnTarget;
    }

    private void OnDestroy()
    {
        PlayerController.OnTarget -= OnTarget;
    }

    private void OnTarget()
    {
        if (playerParticleDust)
        {
            playerParticleDust.Play();
        }        
    }
}
