using DG.Tweening;
using UnityEngine;

namespace AzurGames.UI
{
    public class HintView : MonoBehaviour
    {
        [SerializeField] private CanvasGroup hintGroup;
        [SerializeField] private Animator hintAnimator;

        private float _duration = .2f;

        public void Show()
        {
            hintAnimator.enabled = true;
            gameObject.SetActive(true);
            hintGroup.DOFade(1, _duration);
        }

        public void Hide()
        {
            hintGroup.DOFade(0, _duration).OnComplete(() =>
            {
                gameObject.SetActive(false);
                hintAnimator.enabled = false;
            });
        }
    }
}