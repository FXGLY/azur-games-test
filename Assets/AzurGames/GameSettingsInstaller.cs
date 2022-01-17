using System;
using UnityEngine;
using Zenject;

namespace AzurGames.Main
{
    [CreateAssetMenu(fileName = "Settings", menuName = "ScriptableObjects/Settings")]
    public class GameSettingsInstaller : ScriptableObjectInstaller<GameSettingsInstaller>
    {
        public GameSettings gameSettings;

        public override void InstallBindings()
        {
            Container.BindInstance(gameSettings).IfNotBound();
        }
    }

    [Serializable]
    public class GameSettings
    {
        [Header("Player Settings")] 
        [SerializeField] private float playerForwardMovementSpeed = 5f;
        [SerializeField] private float playerSideMovementSpeed = 12f;
        [SerializeField] private float playerSidePositionLimit = 1.35f;
        
        [Header("Camera Settings")] 
        [SerializeField] private float cameraSmoothSpeed = 0.1f;
        [SerializeField] private Vector3 cameraOffset;

        [Header("Collectables")] 
        [SerializeField] private int totalBonusCount = 3;
        [SerializeField] private int totalCrystalCount = 14;
        [SerializeField] private float minimalOffset = 1.25f;
        [SerializeField] private float minimalSpawnPosition = 3f;
        
        public float PlayerForwardMovementSpeed => playerForwardMovementSpeed;
        public float PlayerSideMovementSpeed => playerSideMovementSpeed;
        public float PlayerSidePositionLimit => playerSidePositionLimit;
        
        public float CameraSmoothSpeed => cameraSmoothSpeed;
        public Vector3 CameraOffset => cameraOffset;

        public int TotalBonusCount => totalBonusCount;
        public int TotalCrystalCount => totalCrystalCount;
        
        public float MinimalOffset => minimalOffset;
        public float MinimalSpawnPosition => minimalSpawnPosition;
    }
}