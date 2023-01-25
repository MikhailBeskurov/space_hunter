using UnityEngine;

namespace SpaceHunter.Scripts.Models.Dialog
{
    public class Message
    {
        public readonly Sprite Avatar;
        public readonly string Name;
        public readonly string Text;
        
        public Message(Sprite avatar, string name, string text)
        {
            Avatar = avatar;
            Name = name;
            Text = text;
        }
    }
}