using System;
using AzurGames.Main;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace AzurGames.UI
{
    public class CrystalCounterView : MonoBehaviour
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

        private void Awake()
        {
            GameManager.OnTakeCrystal += ChangeValue;

            if (PlayerPrefs.HasKey("CrystalCount"))
            {
                _currentCount = PlayerPrefs.GetInt("CrystalCount");
                counterText.text = _currentCount.ToString();
            }
        }

        public void Show()
        {
            if(_isShowed) return;
            
            gameObject.SetActive(true);
            
            canvasGroup.DOFade(1, _duration * .5f);
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
            PlayerPrefs.SetInt("CrystalCount", _currentCount);
            counterText.text = _currentCount.ToString();
        }

        private void OnDestroy()
        {
            GameManager.OnTakeCrystal -= ChangeValue;
        }
    }
}