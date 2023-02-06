using UI.Core.View;
using SpaceHunter.Scripts.UI.Models.Weapon;
using UnityEngine;

namespace SpaceHunter.Scripts.UI.Views.Weapon
{
    public class WeaponView : SimpleScreen<WeaponModel>
    {
        [SerializeField] private Sprite _spriteWeapon;
        
        public override void Bind(WeaponModel model)
        {
           
        }
    }
}