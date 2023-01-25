using System.Resources;
using AI.Core.BehaviorTree;
using UnityEngine;

namespace AI.BehaviorTree.Tasks
{
    public class TaskGoToTarget : AbstractNode
    {
        private Rigidbody _rb;
        private Animator _animator;
        
        private float _speed;

        public TaskGoToTarget(Rigidbody rigidbody, Animator animator, float speed)
        {
            _animator = animator;
            _speed = speed;
            _rb = rigidbody;
        }

        public override NodeState Evaluate()
        { 
            Transform target = (Transform) GetData("target");
            if (Vector3.Distance(_rb.position, target.transform.position) > 3f)
            {
                _rb.position = Vector3.MoveTowards(_rb.position, target.position,_speed*Time.deltaTime);
                _rb.transform.LookAt(new Vector3(target.position.x, _rb.position.y, target.position.z));
                if (!_animator.GetBool("Run"))
                {
                    _animator.SetBool("Run", true);
                }
            }
            else
            {
                if (_animator.GetBool("Run"))
                {
                    _animator.SetBool("Run", false);
                }
            }

            if (Vector3.Distance(_rb.position, target.transform.position) > 25f)
            {
                ClearData("target");
            }

            State = NodeState.Running;
            return State;
        }
    }
}