using UnityEngine;

namespace Utils.Random
{
    public class UnityRandom : IRandomProvider
    {
        public float Next => UnityEngine.Random.value;
        public float Range(float @from, float to)
        {
            return UnityEngine.Random.Range(from, to);
        }

        public int Range(int min, int max)
        {
            return UnityEngine.Random.Range(min, max);
        }
    }
}
