using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SpaceHunter.Scripts.Models.Dialog
{
    [CreateAssetMenu(fileName = "DialogStories", menuName = "Stories/Dialog", order = 0)]
    public class DialogStories : ScriptableObject
    {
        [SerializeField] private List<Story> _stories = new List<Story>();

        public List<MessageSettings> GetScenario(Scenarios scenarios)
        {
            return _stories.FirstOrDefault(v => v.Scenario == scenarios)?.Messages.ToList();
        }
    }

    [Serializable]
    public class Story
    {
        public Scenarios Scenario => _scenario;
        public IReadOnlyCollection<MessageSettings> Messages => _messages;
        
        [SerializeField] private Scenarios _scenario;
        [SerializeField] private List<MessageSettings> _messages = new List<MessageSettings>();
    }
}