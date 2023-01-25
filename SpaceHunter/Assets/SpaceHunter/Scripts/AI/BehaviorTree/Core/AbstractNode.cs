using System.Collections.Generic;

namespace AI.Core.BehaviorTree
{
    public abstract class AbstractNode
    {
        public AbstractNode Parent { get; set; }
        protected NodeState State;
        protected IReadOnlyCollection<AbstractNode> Children => _children;
        
        private List<AbstractNode> _children = new List<AbstractNode>();
        private Dictionary<string, object> _dataContext = new Dictionary<string, object>();

        public AbstractNode()
        {
            Parent = null;
        }

        public AbstractNode(IReadOnlyCollection<AbstractNode> children)
        {
            foreach (var child in children)
            {
                Attach(child);
            }
        }

        public virtual NodeState Evaluate() => NodeState.Failed;
        
        public void SetData(string key, object value)
        {
            _dataContext[key] = value;
        }
        
        public object GetData(string key)
        {
            object value = null;
            if (_dataContext.TryGetValue(key, out value))
            {
                return value;
            }

            AbstractNode node = Parent;
            while (node != null)
            {
                value = node.GetData(key);
                if (value != null)
                {
                    return value;
                }
                node = node.Parent;
            }

            return null;
        }
        
        public bool ClearData(string key)
        {
            if (_dataContext.ContainsKey(key))
            {
                _dataContext.Remove(key);
                return true;
            }

            AbstractNode node = Parent;
            while (node != null)
            {
                bool cleared = node.ClearData(key);
                if (cleared)
                {
                    return true;
                }
                node = node.Parent;
            }
            return false;
        }
        
        private void Attach(AbstractNode node)
        {
            node.Parent = this;
            _children.Add(node);
        }
    }
}