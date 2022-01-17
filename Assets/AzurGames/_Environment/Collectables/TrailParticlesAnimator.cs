using DG.Tweening;
using UnityEngine;

namespace AzurGames.Environment
{
    public class TrailParticlesAnimator : MonoBehaviour
    {
        [SerializeField] private Transform particleTransform;

        [SerializeField] private Vector3 animationVector;

        public void DoTransition()
        {
            particleTransform.DOLocalMove(animationVector, .3f);
        }
    }
}