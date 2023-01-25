using UnityEngine;
using Zenject.ReflectionBaking.Mono.Collections.Generic;

namespace SpaceHunter.Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Bullets", menuName = "SpaceHunter/Bullets", order = 0)]
    public class Bullets : ScriptableObject
    {
        //[SerializeField] private ReadOnlyCollection<Bullet> _bullets;
        [SerializeField] public GameObject _bullet;
        private class Bullet
        {
            public GameObject bullet;
        }
    }
}