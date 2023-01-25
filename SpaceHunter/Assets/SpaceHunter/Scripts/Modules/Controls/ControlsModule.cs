using System;
using UniRx;
using UnityEngine;
using Object = UnityEngine.Object;

namespace SpaceHunter.Scripts.Modules.Controls
{
    public interface IControlsMovement
    {
        public IReadOnlyReactiveProperty<Vector2> VelocityMovement { get; }
    }

    public interface IControlsWeapons
    {
        public event Action Shooting;
        public event Action StopShooting;
        public event Action<int> ScrollWeapon;
    }
    
    public interface IControlsSettings
    {
        public void EnabledMovement();
        public void DisabledMovement();
    }
    
    public class ControlsModule : IControlsMovement, IControlsWeapons, IControlsSettings
    {
        public IReadOnlyReactiveProperty<Vector2> VelocityMovement => _velocityMovement;
        public event Action Shooting;
        public event Action StopShooting;
        public event Action<int> ScrollWeapon;

        private ReactiveProperty<Vector2> _velocityMovement = new ReactiveProperty<Vector2>();
        private PlayerControls _playerControls;

        public ControlsModule(PlayerControls playerControls)
        {
            _playerControls = playerControls;
            InitBindCharacterMovement();
            BindShoot();
            BindSwapWeapon();
            
        }

        public void EnabledMovement()
        {
            _playerControls.Enable();
        }
        
        public void DisabledMovement()
        {
            _playerControls.Disable();
        }
        
        private void InitBindCharacterMovement()
        {
            _playerControls.Character.Movement.performed += context =>
            {
                _velocityMovement.Value = context.ReadValue<Vector2>();
            };
            
            _playerControls.Character.Movement.canceled += context =>
            {
                _velocityMovement.Value = Vector2.zero;
            };
        }
        
        private void BindShoot()
        {
            _playerControls.Character.Shoot.performed += context =>
            {
                Shooting?.Invoke();
            };
            _playerControls.Character.Shoot.canceled += context =>
            {
                StopShooting?.Invoke();
            };
        }
        
        private void BindSwapWeapon()
        {
            _playerControls.Character.ScrollWeapon.performed += context =>
            {
                int direction = context.ReadValue<Vector2>().y >= 0 ? 1 : -1;
                ScrollWeapon?.Invoke(direction);
            };
        }
    }
}