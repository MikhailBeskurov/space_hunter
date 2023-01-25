using System;
using UniRx;
using UnityEngine;

namespace SpaceHunter.Scripts.Modules.Weapon.Weapons
{
    public interface IWeaponBase
    {
    
    }

    public abstract class WeaponBase : IWeaponBase
    {
        protected readonly GameObject _bullet;
        private DateTime _lastUse;
        private float _delay = 150f;
        public WeaponBase(GameObject bullet)
        {
            _bullet = bullet;
            _lastUse = DateTime.Now;
        }

        public void Shoot(ReactiveProperty<float> angle, ReactiveProperty<Vector3> position)
        {
            if (DateTime.Now.Subtract(_lastUse).TotalMilliseconds > _delay)
            {
                Shooting(angle, position);
                _lastUse = DateTime.Now;
            }
        }

        protected abstract void Shooting(ReactiveProperty<float> angle, ReactiveProperty<Vector3> position);
        public abstract void StopShooting();
    }
}