using System.Threading.Tasks;
using UniRx;
using UnityEngine;

namespace SpaceHunter.Scripts.Modules.Weapon.Weapons
{
    public class WeaponRifle : WeaponBase
    {
        private bool _isShooting;
        
        public WeaponRifle(GameObject bullet) : base(bullet)
        {
        }


        protected override async void Shooting(ReactiveProperty<float> angle, ReactiveProperty<Vector3> position)
        {
            _isShooting = true;
            while (_isShooting)
            {
                Object.Instantiate(_bullet, position.Value, Quaternion.AngleAxis(angle.Value-90f, Vector3.up));
                await Task.Delay(150);
            } 
        }

        public override void StopShooting()
        {
            _isShooting = false;
        }
    }
}