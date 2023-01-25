using UnityEngine;

namespace AI.Core.BehaviorTree
{
    public abstract class BehaviorTree : MonoBehaviour
    {
        private AbstractNode _head;

        protected void Start()
        {
            _head = SetupTree();
        }

        private void Update()
        {
            if (_head != null)
            {
                _head.Evaluate();
            }
        }

        protected abstract AbstractNode SetupTree();
    }
}