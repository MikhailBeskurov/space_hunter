using SpaceHunter.Scripts.Models.Dialog;
using SpaceHunter.Scripts.Modules.Controls;
using SpaceHunter.Scripts.Modules.Crosshair;
using SpaceHunter.Scripts.Modules.Dialog;
using Zenject;

namespace SpaceHunter.Scripts.Modules.Gameplay
{
    public interface IGameplayModule
    {
        public void StartGame();
    }
    public interface IGameplayDialog
    {
        public void OpenDialogWindow(Scenarios scenario);
    }

    public class GameplayModule : IGameplayModule, IGameplayDialog
    {
        private DialogModule _dialogModule;
        private IControlsSettings _controlsSettings;
        private ICrosshairModule _crosshairModule;

        [Inject]
        public void Inject(DialogModule dialogModule, IControlsSettings controlsSettings, 
            ICrosshairModule crosshairModule)
        {
            _crosshairModule = crosshairModule;
            _controlsSettings = controlsSettings;
            _dialogModule = dialogModule;
            dialogModule.OnDialogFinish += HideDialogWindow;
        }

        public void StartGame()
        {
            _controlsSettings.EnabledMovement();
        }

        public void OpenDialogWindow(Scenarios scenario)
        {
            _controlsSettings.DisabledMovement();
            _crosshairModule.DisableInput();
            _dialogModule.StartDialog(scenario);
        }

        public void HideDialogWindow()
        {
            _controlsSettings.EnabledMovement();
            _crosshairModule.EnableInput();
        }
    }
}