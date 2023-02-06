using System.Collections.Generic;
using Utils.Update;
using World.Core;
using SpaceHunter.Scripts.Modules.Controls;
using SpaceHunter.Scripts.Modules.Player;
using SpaceHunter.Scripts.Modules.Weapon.Weapons;
using SpaceHunter.Scripts.ScriptableObjects;
using SpaceHunter.Scripts.World.Views.Player;
using UniRx;
using UnityEngine;

namespace SpaceHunter.Scripts.Modules.Weapon
{
    public interface IWeaponModule
    {
        
    }

    public class WeaponModule : IWeaponModule, IUpdatable
    {  
        private int _currentWeapon;
        private ReactiveProperty<float> _angle = new ReactiveProperty<float>();
        private ReactiveProperty<Vector3> _position = new ReactiveProperty<Vector3>();
        
        private Transform _playerView => _worldManager.GetViewInstance<PlayerView>().transform;
        private Transform _crosshairView => _worldManager.GetViewInstance<CrosshairView>().transform;
        
        private IControlsWeapons _controlsWeapons;
        private WorldManager _worldManager;
        private Bullets _bullets;
        
        private List<WeaponBase> _weapons = new List<WeaponBase>();
        
        public WeaponModule(WorldManager worldManager, IControlsWeapons controlsWeapons, Bullets bullets)
        {
            _controlsWeapons = controlsWeapons;
            _bullets = bullets;
            _worldManager = worldManager;
            Init();
            Bind();
        }

        private void Init()
        {
            _weapons.Add(new WeaponRifle(_bullets.Bullet));
            _weapons.Add(new WeaponPistol(_bullets.Bullet));
            _weapons.Add(new WeaponKnife(_bullets.Bullet));
        }

        private void Bind()
        {
            _controlsWeapons.Shooting += Shoot;
            _controlsWeapons.ScrollWeapon += ScrollWeapon;
            _controlsWeapons.StopShooting += StopShooting;
        }

        private void Shoot()
        { 
            _weapons[_currentWeapon].Shoot(_angle, _position);
        }
        
        private void StopShooting()
        {
            _weapons[_currentWeapon].StopShooting();
        }
        
        private void ScrollWeapon(int diffIndex)
        {
            StopShooting();
            _currentWeapon += diffIndex;
            if (_currentWeapon > _weapons.Count - 1)
            {
                _currentWeapon = 0;
            }else if (_currentWeapon < 0)
            {
                _currentWeapon = _weapons.Count - 1;
            }
        }

        public void Update(float deltaTime)
        {
            _angle.Value = Mathf.Rad2Deg * (Mathf.Atan2(_crosshairView.position.x - _playerView.position.x, 
                _crosshairView.position.z - _playerView.position.z));
            _position.Value = _playerView.position;
        }
    }
}