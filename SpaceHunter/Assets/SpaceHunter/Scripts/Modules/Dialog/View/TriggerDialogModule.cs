using System;
using SpaceHunter.Scripts.Models.Dialog;
using SpaceHunter.Scripts.Modules.Gameplay;
using UnityEngine;
using Zenject;

namespace SpaceHunter.Scripts.Modules.Dialog
{
    public class TriggerDialogModule : MonoBehaviour
    {
        private IGameplayModule _gameplayModule;

        [Inject]
        public void Inject(GameplayModule gameplayModule)
        {
            _gameplayModule = gameplayModule;
        }

        public void OnTrigger(Scenarios scenario)
        {
            _gameplayModule.OpenDialogWindow(scenario);
        }
    }
}