using System.Collections.Generic;

namespace AI.Core.BehaviorTree
{
    public abstract class Node
    {
        protected NodeState _state;
        public Node Parent;
        protected List<Node> _children;
    }
}