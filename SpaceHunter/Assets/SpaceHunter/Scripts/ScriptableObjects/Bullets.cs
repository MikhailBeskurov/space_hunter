using UnityEngine;

namespace SpaceHunter.Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Bullets", menuName = "SpaceHunter/Bullets", order = 0)]
    public class Bullets : ScriptableObject
    {
        [SerializeField] private GameObject _bullet;
        public GameObject Bullet => _bullet;
    }
}