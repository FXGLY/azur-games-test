using AzurGames.Main;
using UnityEngine;

namespace AzurGames.Environment
{
    public class FinishTriggerController : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player")) return;

            GameManager.SetState(State.Complete);
        }
    }
}