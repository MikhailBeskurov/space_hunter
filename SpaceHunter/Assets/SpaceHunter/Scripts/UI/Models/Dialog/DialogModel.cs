using System;
using HoneyWood.Scripts.UI.Core.Model;
using SpaceHunter.Scripts.Models.Dialog;
using SpaceHunter.Scripts.Modules.Dialog;
using SpaceHunter.Scripts.UI.Views.DialogScreen;
using UniRx;
using UnityEngine;

namespace SpaceHunter.Scripts.UI.Models.Dialog
{
    public class DialogModel : AbstractScreenModel
    {
        public IReadOnlyReactiveCollection<Message> Messages;
        private IDialogModule _dialogModule;


        public DialogModel(IDialogModule dialogModule)
        {
            _dialogModule = dialogModule;
            Messages = dialogModule.Messages;
            dialogModule.OnDialogFinish += (() =>
            {
                _uiManager.Hide<DialogModel>();
            });
        }

        public void Hide()
        {
            _dialogModule.HideDialogView();
            _uiManager.Hide<DialogModel>();
        }
    }
}