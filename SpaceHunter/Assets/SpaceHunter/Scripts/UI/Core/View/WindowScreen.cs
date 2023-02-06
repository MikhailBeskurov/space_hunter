using UI.Core.Model;

namespace UI.Core.View
{
    public abstract class WindowScreen<TModel> : BaseScreen<TModel> where TModel : IUIModel
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