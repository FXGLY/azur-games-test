using System.Collections.Generic;
using AzurGames.Main;
using UnityEngine;
using Zenject;

namespace AzurGames.Environment
{
    public class CollectablesSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject crystalPrefab;
        [SerializeField] private GameObject bonusPrefab;

        private GameSettings _gameSettings;
        
        private List<GameObject> _collectablesList = new List<GameObject>();
        private List<float> _sidePositions = new List<float>{-1.25f, 0, 1.25f};
        private List<float> _forwardPositions = new List<float>();
        private List<float> _tempForwardPositions = new List<float>();

        [Inject]
        private void Inject(GameSettings gameSettings)
        {
            _gameSettings = gameSettings;
        }

        private void Awake()
        {
            for (var i = 1; i < 42; i++)
            {
                var position = _gameSettings.MinimalSpawnPosition + _gameSettings.MinimalOffset * i;
                _forwardPositions.Add(position);
            }

            _tempForwardPositions = new List<float>(_forwardPositions);
            
            GameManager.OnRestartLevel += Spawn;
            Spawn();
        }

        private void Spawn()
        {
            Clear();
            
            SpawnBonusItems();
            SpawnCrystalItems();
        }

        private void SpawnBonusItems()
        {
            for (var i = 0; i < _gameSettings.TotalBonusCount; i++)
            {
                var a = Random.Range(0, _sidePositions.Count);
                var b = Random.Range(0, _tempForwardPositions.Count);

                var bonusSidePosition = _sidePositions[a];
                var bonusForwardPosition = _tempForwardPositions[b];
                
                var bonusObject = Instantiate(
                    bonusPrefab, 
                    new Vector3(bonusSidePosition,0,bonusForwardPosition),
                    Quaternion.identity,
                    transform);
                
                _collectablesList.Add(bonusObject);
                _tempForwardPositions.RemoveAt(b);
            }
        }

        private void SpawnCrystalItems()
        {
            for (var i = 0; i < _gameSettings.TotalCrystalCount; i++)
            {
                var a = Random.Range(0, _sidePositions.Count);
                var b = Random.Range(0, _tempForwardPositions.Count);

                var crystalSidePosition = _sidePositions[a];
                var crystalForwardPosition = _tempForwardPositions[b];
                
                var crystalObject = Instantiate(
                    crystalPrefab, 
                    new Vector3(crystalSidePosition,0,crystalForwardPosition),
                    Quaternion.identity,
                    transform);
                
                _collectablesList.Add(crystalObject);
                _tempForwardPositions.RemoveAt(b);
            }
        }

        private void Clear()
        {
            foreach (var item in _collectablesList)
            {
                Destroy(item);
            }
            
            _collectablesList.Clear();

            _tempForwardPositions = new List<float>(_forwardPositions);
        }
    }
}