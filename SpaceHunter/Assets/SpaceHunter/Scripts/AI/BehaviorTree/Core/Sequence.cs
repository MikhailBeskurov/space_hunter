using System.Collections.Generic;

namespace AI.Core.BehaviorTree
{
    public class Sequence : AbstractNode
    {
        public Sequence() : base()
        {
        }

        public Sequence(List<AbstractNode> children) : base(children)
        {
            
        }

        public override NodeState Evaluate()
        {
            bool anyChildIsRunning = false;

            foreach (var child in Children)
            {
                switch (child.Evaluate())
                {
                    case NodeState.Failed:
                        State = NodeState.Failed;
                        return State;
                    case NodeState.Success:
                        continue;
                    case NodeState.Running:
                        anyChildIsRunning = true;
                        continue;
                    default:
                        State = NodeState.Success;
                        return State;
                }
            }

            State = anyChildIsRunning ? NodeState.Running : NodeState.Success;
            return State;
        }
    }
}