using System;
using Cinemachine;
using World.Core.View;
using SpaceHunter.Scripts.World.Models.Player;
using UnityEngine;
using UniRx;

namespace SpaceHunter.Scripts.World.Views.Player
{
    public class CrosshairView : SimpleView<CrosshairModel>
    {
        private CinemachineVirtualCamera _cinemachineVirtualCamera;
        private Vector3 _positionMouse;
        private Vector3 _pastPositionMouse;
        private Vector3 _nextPositionMouse;
        private CrosshairModel _model;

        public void Awake()
        {
            _cinemachineVirtualCamera = FindObjectOfType(typeof(CinemachineVirtualCamera)) as CinemachineVirtualCamera;
            _cinemachineVirtualCamera!.m_Follow = transform;
        }

        public override void Bind(CrosshairModel model)
        {
            _model = model;
            model.MousePosition.Subscribe(v =>
            { 
                _nextPositionMouse = v;
            });
        }

        private void Update()
        {
            _positionMouse += (_nextPositionMouse - _pastPositionMouse);
            _positionMouse = Vector3.ClampMagnitude(_positionMouse, 5);
            
            _positionMouse.y = 0;
            transform.position = _model.PlayerPosition + _positionMouse;
            
            _pastPositionMouse = _nextPositionMouse;
        }
    }
}