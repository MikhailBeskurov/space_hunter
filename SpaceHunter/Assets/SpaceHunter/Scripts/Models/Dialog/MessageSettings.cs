using System;
using UnityEngine;

namespace SpaceHunter.Scripts.Models.Dialog
{
    [Serializable]
    public class MessageSettings
    {
        public CharactersType Character;
        [TextArea()]
        public string Text;
    }
}