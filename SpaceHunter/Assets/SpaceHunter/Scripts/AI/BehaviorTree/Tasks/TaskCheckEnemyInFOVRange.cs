using AI.Core.BehaviorTree;
using UnityEditor;
using UnityEngine;

namespace AI.BehaviorTree.Tasks
{
    public class TaskCheckEnemyInFOVRange : AbstractNode
    {
        private int _enemyLayerMask = 1 << 7;
        private Transform _transform;
        private float _rangeFOV;

        public TaskCheckEnemyInFOVRange(Transform transform, float rangeFOV)
        {
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