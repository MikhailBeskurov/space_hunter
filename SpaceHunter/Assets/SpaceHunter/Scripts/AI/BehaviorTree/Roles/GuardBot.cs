using System;
using AI.BehaviorTree.Tasks;
using AI.Core.BehaviorTree;
using UnityEngine;

namespace AI.BehaviorTree.Roles
{
    public class GuardBot : Core.BehaviorTree.BehaviorTree
    {
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private Transform[] _waypoints;
        [SerializeField] private float _speed;
        [SerializeField] private float _waitingTime;

        private void OnValidate()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        protected override AbstractNode SetupTree()
        {
            AbstractNode head = new TaskPatrol(_rigidbody, _waypoints, 
                _speed, _waitingTime);
            return head;
        }
    }
}