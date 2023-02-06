using World.Core.Model;

namespace World.Core.View
{
    public interface IView
    {
        bool IsShown { get; }
        void Show();
        void Hide();
    }

    public interface IView<T> : IView where T : IViewModel
    {
        void Bind(T model);
    }
}
