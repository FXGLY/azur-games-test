using AzurGames.Main;
using UnityEngine;
using Zenject;

namespace AzurGames.Player
{
    public class CameraController : MonoBehaviour
    {

        [SerializeField] private Transform target;
        
        private GameSettings _gameSettings;

        [Inject]
        private void Inject(GameSettings gameSettings)
        {
            _gameSettings = gameSettings;
        }

        private void LateUpdate()
        {
            var targetPosition = target.position + _gameSettings.CameraOffset;
            var smoothedPosition = Vector3.Lerp(transform.position, targetPosition, _gameSettings.CameraSmoothSpeed);
            transform.position = new Vector3(0, smoothedPosition.y, smoothedPosition.z);
        }
    }
}