﻿using DG.Tweening;
using HoneyWood.Scripts.UI.Core.Model;

namespace HoneyWood.Scripts.UI.Core.View
{
    public abstract class SimpleScreen<TModel> : BaseScreen<TModel> where TModel : IUIModel
    {
        public override bool IsShown => gameObject.activeSelf;
        
        public override void Show()
        {
            gameObject.SetActive(true);
        }

        public override void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}
