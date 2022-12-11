using System;
using DG.Tweening;
using DG.Tweening.Core;
using HoneyWood.Scripts.UI.Core.Model;
using UnityEngine;

namespace HoneyWood.Scripts.UI.Core.View
{
    public abstract class AnimationScreen<TModel> : BaseScreen<TModel> where TModel : IUIModel
    {
        //[SerializeField] private DOTweenAnimation _animation;
        public override bool IsShown { get; }

        private const float _animationTime = 0.3f;
        
        public override void Show()
        {
            UIManager.UIShown.Value = true;
            UIManager.BlockingCount++;
            //_animation.DORestart();
        }

        public override void Hide()
        {
            if (IsShown)
            {
                UIManager.BlockingCount--;
            }
            if (UIManager.BlockingCount <= 0)
            {
                UIManager.BlockingCount = 0;
                UIManager.UIShown.Value = false;
            }
            else
            {
                UIManager.UIShown.Value = true;
            }
            gameObject.SetActive(false);
        }

        protected virtual void AnimationHide()
        {
            transform.DOScale(0.3f, _animationTime).From(1).OnComplete(Hide);
        }
    }
}