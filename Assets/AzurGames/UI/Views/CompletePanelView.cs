using AzurGames.Main;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace AzurGames.UI
{
    public class CompletePanelView : MonoBehaviour
    {
        [SerializeField] private RectTransform rectTransform;
        [SerializeField] private Button restartButton;
        [SerializeField] private TMP_Text title;
        [SerializeField] private Image backgroundImage;
        
        [Header("Groups")]
        [SerializeField] private CanvasGroup mainCanvasGroup;
        [SerializeField] private CanvasGroup buttonCanvasGroup;
        [SerializeField] private CanvasGroup bonusCounterCanvasGroup;
        [SerializeField] private CanvasGroup crystalCounterCanvasGroup;

        [Header("Inner Elements")] 
        [SerializeField] private TMP_Text bonusCounterText;
        [SerializeField] private TMP_Text crystalCounterText;

        [Header("Animation Settings")]
        [SerializeField] private Vector2 hidedAnchorMin;
        [SerializeField] private Vector2 hidedAnchorMax;

        [SerializeField] private Vector2 showedAnchorMin;
        [SerializeField] private Vector2 showedAnchorMax;
        
        private float _duration = .325f;
        
        private GameSettings _gameSettings;

        [Inject]
        private void Inject(GameSettings gameSettings)
        {
            _gameSettings = gameSettings;
        }

        private void Awake()
        {
            restartButton.onClick.AddListener(GameManager.OnRestartLevelInvoke);
        }

        public void Show()
        {
            gameObject.SetActive(true);

            SetValues();
            
            var sequence = DOTween.Sequence();

            sequence
                .Append(rectTransform.DOAnchorMin(showedAnchorMin, _duration * 2).From(hidedAnchorMin).SetEase(Ease.OutBack))
                .Join(backgroundImage.DOFade(.8f, _duration * 2).From(0))
                .Join(rectTransform.DOAnchorMax(showedAnchorMax, _duration * 2).From(hidedAnchorMax).SetEase(Ease.OutBack))
                .Join(rectTransform.DOShakeRotation(_duration * 2f, 10, 4))
                .Join(mainCanvasGroup.DOFade(1, _duration))
                .Append(title.DOFade(1, _duration).From(0))
                .Append(bonusCounterCanvasGroup.DOFade(1, _duration).From(0))
                .Append(crystalCounterCanvasGroup.DOFade(1, _duration).From(0))
                .Append(buttonCanvasGroup.DOFade(1, _duration).From(0))
                .OnComplete(() =>
                {
                    restartButton.interactable = true;
                    sequence.Kill();
                });
        }

        public void Hide()
        {
            var sequence = DOTween.Sequence();

            sequence
                .Append(mainCanvasGroup.DOFade(0, _duration))
                .Join(backgroundImage.DOFade(0, _duration))
                .OnComplete(() =>
                {
                    restartButton.interactable = false;
                    gameObject.SetActive(false);
                    sequence.Kill();
                });
        }

        private void SetValues()
        {
            if (PlayerPrefs.HasKey("BonusCount"))
            {
                bonusCounterText.text = PlayerPrefs.GetInt("BonusCount") + "/" + _gameSettings.TotalBonusCount;
            }
            
            if (PlayerPrefs.HasKey("CrystalCount"))
            {
                crystalCounterText.text = PlayerPrefs.GetInt("CrystalCount").ToString();
            }
        }

        private void OnDestroy()
        {
            restartButton.onClick.RemoveAllListeners();
        }
    }
}