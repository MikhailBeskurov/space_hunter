using System.Collections.Generic;
using SpaceHunter.Scripts.Models.Dialog;

namespace SpaceHunter.Scripts.Modules.Dialog
{
    public interface IDialogProvider
    {
        public List<Message> GetDialogScenario(Scenarios scenario);
    }

    public class DialogProvider : IDialogProvider
    {
        private DialogStories _dialogStories;
        private Characters _characters;

        public DialogProvider(DialogStories dialogStories, Characters characters)
        {
            _characters = characters;
            _dialogStories = dialogStories;
        }

        public List<Message> GetDialogScenario(Scenarios scenario)
        {
            List<MessageSettings> messages = _dialogStories.GetScenario(scenario);
            List<Message> messageDialogs = new List<Message>();
            
            foreach (var message in messages)
            {
                var character = _characters.GetCharacterByType(message.Character);
                messageDialogs.Add(new Message(character.Avatar, character.Name, message.Text));
            }
            return messageDialogs;
        }
    }
}