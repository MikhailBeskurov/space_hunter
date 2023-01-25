using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SpaceHunter.Scripts.Models.Dialog
{
    [CreateAssetMenu(fileName = "Characters", menuName = "Stories/Characters", order = 0)]
    public class Characters : ScriptableObject
    {
        [SerializeField] private List<Character> _characters = new List<Character>();

        public Character GetCharacterByType(CharactersType characterType)
        {
            return _characters.FirstOrDefault(v => v.CharactersType == characterType);
        }
    }
    
    [Serializable]
    public class Character
    {
        public CharactersType CharactersType => _charactersType;
        public Sprite Avatar => _avatar;
        public string Name => _name;
            
        [SerializeField] private CharactersType _charactersType;
        [SerializeField] private Sprite _avatar;
        [SerializeField] private string _name;
    }
}