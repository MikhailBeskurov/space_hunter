using System;
using HoneyWood.Scripts.UI.Core.Model;

namespace HoneyWood.Scripts.UI.Core.View
{
    public abstract class BaseScreen<TModel> : AbstractScreen, IScreen<TModel> where TModel : IUIModel
    {
        public override Type ModelType => typeof(TModel);

        public abstract void Bind(TModel model);

        protected virtual void OnShowDone()
        {
        }

        protected virtual void OnHideDone()
        {
        }
    }
}
