using System.Collections.Generic;
using AI.BehaviorTree.Tasks;
using AI.Core.BehaviorTree;
using UnityEngine;

namespace AI.BehaviorTree.Roles
{
    public class GuardBot : Core.BehaviorTree.BehaviorTree
    {
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private Animator _animator;
        [SerializeField] private Transform[] _waypoints;
        [SerializeField] private float _speed = 8f;
        [SerializeField] private float _rangeFOV = 5f;
        [SerializeField] private float _waitingTime = 2f;

        private void OnValidate()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        protected override AbstractNode SetupTree()
        {
            AbstractNode head = new Selector(new List<AbstractNode>
            {
                new Sequence(new List<AbstractNode>
                {
                    new TaskCheckEnemyInFOVRange(transform, _rangeFOV),
                    new TaskGoToTarget(_rigidbody,_animator,_speed)
                }),
                new TaskPatrol(_rigidbody, _waypoints, _animator,
                    _speed, _waitingTime)
            });
            
            return head;
        }
    }
}