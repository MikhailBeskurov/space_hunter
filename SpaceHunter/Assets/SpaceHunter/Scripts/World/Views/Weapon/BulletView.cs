using System;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace SpaceHunter.Scripts.World.Views.Weapon
{
    public class BulletView : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rigidbody;
        
        private void Update()
        {
            _rigidbody.velocity = transform.rotation * (Vector3.right * 60f);
        }
    }
}