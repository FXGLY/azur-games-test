using AzurGames.Main;
using UnityEngine;

namespace AzurGames.Environment
{
    public class BonusController : MonoBehaviour
    {
        [SerializeField] private GameObject particles;

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player")) return;

            GameManager.OnTakeCollectableInvoke();
            particles.SetActive(true);
            Destroy(gameObject);
        }
    }
}