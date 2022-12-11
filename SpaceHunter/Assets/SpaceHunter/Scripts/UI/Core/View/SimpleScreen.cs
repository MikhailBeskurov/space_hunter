using DG.Tweening;
using HoneyWood.Scripts.UI.Core.Model;

namespace HoneyWood.Scripts.UI.Core.View
{
    public abstract class SimpleScreen<TModel> : BaseScreen<TModel> where TModel : IUIModel
    {
        public override bool IsShown => gameObject.activeSelf;
        
        public override void Show()
        {
            UIManager.UIShown.Value = true;
            UIManager.BlockingCount++;
            gameObject.SetActive(true);
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
    }
}
