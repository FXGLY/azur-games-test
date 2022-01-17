using AzurGames.Main;
using DG.Tweening;
using TMPro;
using UnityEngine;
using Zenject;

namespace AzurGames.UI
{
    public class BonusCounterView : MonoBehaviour
    {
        [Header("Elements")] 
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private RectTransform counterRectTransform;
        [SerializeField] private TMP_Text counterText;

        [Header("Animation Settings")]
        [SerializeField] private Vector2 hidedAnchorMin;
        [SerializeField] private Vector2 hidedAnchorMax;

        [SerializeField] private Vector2 showedAnchorMin;
        [SerializeField] private Vector2 showedAnchorMax;

        private int _currentCount;
        private float _duration = .25f;
        private bool _isShowed;
        
        private GameSettings _gameSettings;

        [Inject]
        private void Inject(GameSettings gameSettings)
        {
            _gameSettings = gameSettings;
        }

        private void Awake()
        {
            GameManager.OnTakeCollectable += ChangeValue;
            GameManager.OnRestartLevel += Reset;
            
            SetCounterTextValue(_gameSettings.TotalBonusCount, _currentCount);
        }

        public void Show()
        { 
            if(_isShowed) return;
            
            gameObject.SetActive(true);
            
            canvasGroup.DOFade(1, _duration);
            counterRectTransform.DOAnchorMin(showedAnchorMin, _duration).From(hidedAnchorMin).SetEase(Ease.OutBack);
            counterRectTransform.DOAnchorMax(showedAnchorMax, _duration).From(hidedAnchorMax).SetEase(Ease.OutBack);

            _isShowed = true;
        }

        public void Hide()
        {
            canvasGroup.DOFade(0, _duration).OnComplete(() =>
            {
                gameObject.SetActive(false);
            });
            
            _isShowed = false;
        }

        private void ChangeValue()
        {
            _currentCount++;
            SetCounterTextValue(_gameSettings.TotalBonusCount, _currentCount);
            
            PlayerPrefs.SetInt("BonusCount", _currentCount);
        }

        private void SetCounterTextValue(int totalCount, int currentCount) => counterText.text = currentCount + "/" + totalCount;

        private void Reset()
        {
            _currentCount = 0;
            _isShowed = false;
            SetCounterTextValue(_gameSettings.TotalBonusCount, _currentCount);

            if (PlayerPrefs.HasKey("BonusCount"))
            {
                PlayerPrefs.SetInt("BonusCount", _currentCount);
            }
        }
    }
}