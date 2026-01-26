using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace _Archero.Develop.Runtime.UI.Core
{
    public class PopupAnimationCreator
    {
        public static Sequence CreateShowAnimation(
            CanvasGroup body, 
            Image antiClicker,
            PopupAnimationType animationType, 
            float antiClickerMaxAlpha)
        {
            switch (animationType)
            {
                case PopupAnimationType.None:
                    return DOTween.Sequence();

                case PopupAnimationType.Expand:
                    return DOTween.Sequence()
                        .Append(antiClicker
                            .DOFade(antiClickerMaxAlpha, 0.2f)
                            .From(0))
                        .Join(body.transform
                            .DOScale(1, 0.5f)
                            .From(0)
                            .SetEase(Ease.OutBack));

                default:
                    throw new ArgumentException(nameof(animationType));
            }
        }

        public static Sequence CreateHideAnimation(
            CanvasGroup body, 
            Image antiClicker,
            PopupAnimationType animationType, 
            float antiClickerMaxAlpha)
        {
            return DOTween.Sequence();
        }
    }
}
