using HoneyWood.Scripts.Utils.Update;
using UniRx;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SpaceHunter.Scripts.Modules.Crosshair
{
    public interface ICrosshairModule
    {
        public IReadOnlyReactiveProperty<Vector3> PositionMouse { get; }
        public void EnableInput();
        public void DisableInput();
    }

    public class CrosshairModule : ICrosshairModule, IUpdatable
    {
        public IReadOnlyReactiveProperty<Vector3> PositionMouse => _positionMouse;

        private ReactiveProperty<Vector3> _positionMouse = new ReactiveProperty<Vector3>();

        private float _sensivity = 20f;
        private bool _enableInput = true;
        
        public CrosshairModule()
        {
        }

        public void Update(float deltaTime)
        {
            if (_enableInput)
            {
                Vector3 mousePosition = Mouse.current.position.ReadValue();
                _positionMouse.Value = new Vector3(mousePosition.x, 0, mousePosition.y) / _sensivity;
            }
        }
        
        public void EnableInput()
        {
            _enableInput = true;
        }

        public void DisableInput()
        {
            _enableInput = false;
        }
    }
}