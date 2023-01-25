using UniRx;
using UnityEngine;

namespace SpaceHunter.Scripts.Modules.Weapon.Weapons
{
    public class WeaponPistol : WeaponBase
    {
        public WeaponPistol(GameObject bullet) : base(bullet)
        {
        }

        protected override void Shooting(ReactiveProperty<float> angle, ReactiveProperty<Vector3> position)
        {
            Object.Instantiate(_bullet, position.Value, Quaternion.AngleAxis(angle.Value-90f, Vector3.up));
        }

        public override void StopShooting()
        {
            
        }
    }
}