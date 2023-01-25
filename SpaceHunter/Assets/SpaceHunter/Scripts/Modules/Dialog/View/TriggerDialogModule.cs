using SpaceHunter.Scripts.Models.Dialog;
using SpaceHunter.Scripts.Modules.Gameplay;
using UnityEngine;
using Zenject;

namespace SpaceHunter.Scripts.Modules.Dialog.View
{
    public class TriggerDialogModule : MonoBehaviour
    {
        private IGameplayDialog _gameplayModule;

        [Inject]
        public void Inject(IGameplayDialog gameplayModule)
        {
            _gameplayModule = gameplayModule;
        }

        public void OnTrigger(Scenarios scenario)
        {
            _gameplayModule.OpenDialogWindow(scenario);
        }
    }
}