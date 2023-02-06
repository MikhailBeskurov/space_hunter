namespace UI.Core.Model
{
    public abstract class AbstractScreenModel : IUIModel
    {
        protected IUIManager _uiManager;

        public void SetManager(IUIManager manager)
        {
            _uiManager = manager;
        }
    }

    public abstract class AbstractWorldScreenModel : IWorldUIModel
    {
        protected IUIManager _uiManager;

        public void SetManager(IUIManager manager)
        {
            _uiManager = manager;
        }
    }
}
