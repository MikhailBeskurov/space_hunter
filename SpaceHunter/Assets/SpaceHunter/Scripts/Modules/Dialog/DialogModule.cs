using System;
using SpaceHunter.Scripts.Models.Dialog;
using UniRx;
using Unity.VisualScripting;

namespace SpaceHunter.Scripts.Modules.Dialog
{
    public interface IDialogModule
    {
        public IReadOnlyReactiveCollection<Message> Messages { get; }
        public void StartDialog(Scenarios scenario);
        public void HideDialogView();
        public event Action OnDialogFinish;
    }
    
    public class DialogModule : IDialogModule
    {
        public IReadOnlyReactiveCollection<Message> Messages => _messages;
        public event Action OnDialogFinish;
        
        private ReactiveCollection<Message> _messages = new ReactiveCollection<Message>();
        private IDialogProvider _dialogProvider;

        public DialogModule(IDialogProvider dialogProvider)
        {
            _dialogProvider = dialogProvider;
        }

        public void StartDialog(Scenarios scenario)
        {
            _messages.Clear();
            _messages.AddRange(_dialogProvider.GetDialogScenario(scenario));
        }
        
        public void HideDialogView()
        {
            OnDialogFinish?.Invoke();
        }
    }
}