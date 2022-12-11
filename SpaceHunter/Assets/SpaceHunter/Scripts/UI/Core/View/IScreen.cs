using HoneyWood.Scripts.UI.Core.Model;

namespace HoneyWood.Scripts.UI.Core.View
{
    public interface IScreen
    {
        bool IsShown { get; }
        void Show();
        void Hide();
    }

    public interface IScreen<TModel> : IScreen where TModel : IUIModel
    {
        void Bind(TModel model);
    }
}
