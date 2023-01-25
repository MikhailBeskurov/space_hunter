using System.Collections.Generic;

namespace AI.Core.BehaviorTree
{
    public class Selector : AbstractNode
    {
        public Selector() : base()
        {
        }

        public Selector(List<AbstractNode> children) : base(children)
        {
            
        }

        public override NodeState Evaluate()
        {
            foreach (var child in Children)
            {
                switch (child.Evaluate())
                {
                    case NodeState.Failed:
                        continue;
                    case NodeState.Success:
                        State = NodeState.Success;
                        return State;
                    case NodeState.Running:
                        State = NodeState.Running;
                        return State;
                    default:
                        continue;
                }
            }

            State = NodeState.Failed;
            return State;
        }
    }
}