using System;
using AzurGames.Main;
using Zenject;

namespace AzurGames.Player
{
    using UnityEngine;
    
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private Transform playerTransform;
        [SerializeField] private Animator playerAnimator;
        
        private static readonly int IsRunning = Animator.StringToHash("isRunning");
        private bool _canMove = true;
        private float _positionX;

        private GameSettings _gameSettings;

        [Inject]
        private void Inject(GameSettings gameSettings)
        {
            _gameSettings = gameSettings;
        }

        private void Awake()
        {
            GameManager.OnStateChange += OnLevelComplete;
            GameManager.OnRestartLevel += Respawn;
        }

        private void Update()
        {
            if(!_canMove) return;
            
            if (Input.GetMouseButtonDown(0))
            {
                StartMove();
            }
            else if (Input.GetMouseButton(0))
            {
                Move();
            }
            else if (Input.GetMouseButtonUp(0))
            {
                StopMove();
            }
        }

        private void StartMove()
        {
            playerAnimator.SetBool(IsRunning, true);
            GameManager.SetState(State.Play);
        }

        private void Move()
        {
            playerTransform.position += Vector3.forward * Time.deltaTime * _gameSettings.PlayerForwardMovementSpeed;

            _positionX += Input.GetAxis("Mouse X") * Time.deltaTime * _gameSettings.PlayerSideMovementSpeed;
            
            if(_positionX > _gameSettings.PlayerSidePositionLimit || _positionX < -_gameSettings.PlayerSidePositionLimit) return;

            playerTransform.position = new Vector3(_positionX, playerTransform.position.y, playerTransform.position.z);
        }

        private void StopMove()
        {
            playerAnimator.SetBool(IsRunning, false);
            GameManager.SetState(State.Stop);
        }

        private void OnLevelComplete(State state)
        {
            if(state != State.Complete) return;
            
            playerAnimator.SetBool(IsRunning, false);
            _canMove = false;
        }

        private void Respawn()
        {
            playerTransform.position = Vector3.zero;
            _canMove = true;
            _positionX = 0;
            GameManager.SetState(State.Start);
        }

        private void OnDestroy()
        {
            GameManager.OnStateChange -= OnLevelComplete;
            GameManager.OnRestartLevel -= Respawn;
        }
    }
}