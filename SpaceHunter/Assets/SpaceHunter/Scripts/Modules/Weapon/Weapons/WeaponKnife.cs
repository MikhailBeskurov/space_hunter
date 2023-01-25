using UniRx;
using UnityEngine;

namespace SpaceHunter.Scripts.Modules.Weapon.Weapons
{
    public class WeaponKnife : WeaponBase
    {
        public WeaponKnife(GameObject bullet) : base(bullet)
        {
        }


        protected override void Shooting(ReactiveProperty<float> angle, ReactiveProperty<Vector3> position)
        {
            Debug.Log("Knife!");
        }

        public override void StopShooting()
        {
            
        }
    }
}