﻿using World.Core.Model;

namespace World.Core.View
{
    public abstract class SimpleView<T> : BaseView<T> where T : IViewModel
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
