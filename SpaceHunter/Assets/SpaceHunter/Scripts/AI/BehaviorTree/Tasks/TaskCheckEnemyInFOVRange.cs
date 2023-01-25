using AI.Core.BehaviorTree;
using UnityEditor;
using UnityEngine;

namespace AI.BehaviorTree.Tasks
{
    public class CheckEnemyInFOVRange : AbstractNode
    {
        private int _enemyLayerMask = 1 << 5;
        private Transform _transform;
        private float _rangeFOV;
        private Animator _animator;

        public CheckEnemyInFOVRange(Transform transform, Animator animator, float rangeFOV)
        {
            _animator = animator;
            _rangeFOV = rangeFOV;
            _transform = transform;
        }

        public override NodeState Evaluate()
        {
            object t = GetData("target");
            if (t == null)
            {
                Collider[] colliders = Physics.OverlapSphere(_transform.position, _rangeFOV, _enemyLayerMask);
                
                if (colliders.Length > 0)
                {
                    Parent.Parent.SetData("target", colliders[0].transform);
                    _animator.SetBool("Run", true);
                    State = NodeState.Success;
                    return State;
                }
                State = NodeState.Failed;
                return State;
            }

            State = NodeState.Success;
            return State;
        }
    }
}