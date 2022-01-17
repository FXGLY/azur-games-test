using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

namespace AzurGames.UICommon
{
    [RequireComponent(typeof(RectTransform))]
    public class ButtonScaleClickEffect : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        [Range(0, 1)]
        [SerializeField] private float transitionTime;
        [Range(0, 2)]
        [SerializeField] private float scaleFactor;
        
        private RectTransform _rectTransform;
        private Vector3 _defaultSizeDelta;
        private Tweener _tweener;

        private void Awake() {
            _rectTransform = GetComponent<RectTransform>();
            _defaultSizeDelta = _rectTransform.sizeDelta;
        }

        private void OnEnable() => Reset();

        public  void OnPointerDown(PointerEventData eventData) {
            _tweener = _rectTransform.DOSizeDelta(_defaultSizeDelta * scaleFactor, transitionTime);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _tweener = _rectTransform.DOSizeDelta(_defaultSizeDelta, transitionTime);
        }

        private void Reset()
        {
            _tweener.Kill();
            _rectTransform.sizeDelta = _defaultSizeDelta;
        }
    }
}
