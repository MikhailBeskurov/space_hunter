using System;
using UI.Core.Model;

namespace UI.Core.View
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
