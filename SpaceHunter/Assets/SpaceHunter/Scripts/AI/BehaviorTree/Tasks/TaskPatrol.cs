using System;
using System.Timers;
using AI.Core.BehaviorTree;
using UnityEngine;

namespace AI.BehaviorTree.Tasks
{
    public class TaskPatrol : AbstractNode
    {
        private Rigidbody _rb;
        private Transform[] _waypoints;
        
        private float _speed;
        private float _waitingTime;
        
        private int _currentWaypointIndex = 0;
        private bool _waiting = false;
        private DateTime _startWaiting;
        private Animator _animator;
        private readonly int Run = Animator.StringToHash("Run");

        public TaskPatrol(Rigidbody rigidbody, Transform[] waypoints, Animator animator,
            float speed, float waitingTime)
        {
            _animator = animator;
            _waypoints = waypoints;
            _rb = rigidbody;
            _speed = speed;
            _waitingTime = waitingTime;
        }

        public override NodeState Evaluate()
        {
            if (_waiting)
            {
                if (DateTime.Now.Subtract(_startWaiting).TotalSeconds >= _waitingTime)
                {
                    _waiting = false;
                    _animator.SetBool(Run, true);
                }
            }
            else
            {
                Transform wayPoint = _waypoints[_currentWaypointIndex];
                if (Vector3.Distance(_rb.position, wayPoint.position) < 1f)
                {
                    _rb.position = wayPoint.position;
                    _startWaiting = DateTime.Now;
                    _waiting = true;
                    _currentWaypointIndex = (_currentWaypointIndex + 1) % _waypoints.Length;
                    _animator.SetBool(Run, false);
                }
                else
                {
                    // _rb.velocity = (wayPoint.position - _rb.position).normalized * _speed;
                    _rb.position = Vector3.MoveTowards(_rb.position, wayPoint.position,_speed*Time.deltaTime);
                    _rb.transform.LookAt(new Vector3(wayPoint.position.x, _rb.position.y,wayPoint.position.z));
                }
            }

            State = NodeState.Running;
            return State;
        }
    }
}