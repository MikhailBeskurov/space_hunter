using System;
using UnityEngine;

namespace SpaceHunter.Scripts.Models.Dialog
{
    [Serializable]
    public class Message
    {
        public CharactersType Character;
        [TextArea()]
        public string Text;
    }
}