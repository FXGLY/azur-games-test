using AzurGames.Main;
using UnityEngine;

namespace AzurGames.Environment
{
    public class CrystalController : MonoBehaviour
    {
        [SerializeField] private GameObject particles;
        [SerializeField] private TrailParticlesAnimator trailParticles;

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player")) return;

            GameManager.OnTakeCrystalInvoke();
            particles.SetActive(true);
            trailParticles.DoTransition();
            Destroy(gameObject);
        }
    }
}