using System.Collections.Generic;
using System.Linq;
using HoneyWood.Scripts.UI.Core.View;
using SpaceHunter.Scripts.Models.Dialog;
using SpaceHunter.Scripts.Modules.Dialog;
using SpaceHunter.Scripts.UI.Models.Dialog;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

namespace SpaceHunter.Scripts.UI.Views.DialogScreen
{
    public class DialogScreen : SimpleScreen<DialogModel>
    {
        [SerializeField] private Image _avatar;
        [SerializeField] private TMP_Text _name;
        [SerializeField] private TMP_Text _text;
        [SerializeField] private Button _buttonNext;
        
        private List<Message> _messageDialogs = new List<Message>();
        private DialogModel _model;

        public override void Bind(DialogModel model)
        {
            _model = model;
            model.Messages.ObserveAdd().Subscribe(v =>
            {
                _messageDialogs.Add(v.Value);
                if (!gameObject.activeSelf)
                {
                    Show();
                    UpdateDialog();
                }
            });
            _buttonNext.onClick.AddListener(() =>
            {
                _messageDialogs.RemoveAt(0);
                if (_messageDialogs.Count == 0)
                {
                    _model.Hide();
                    return;
                }
                UpdateDialog();
            });
        }

        public override void Hide()
        {
            if (_messageDialogs.Count > 0)
            {
                _messageDialogs.Clear();
            }
            
            base.Hide();
        }

        private void UpdateDialog()
        {
            _avatar.sprite = _messageDialogs[0].Avatar;
            _name.text = _messageDialogs[0].Name;
            _text.text = _messageDialogs[0].Text;
        }
    }
}