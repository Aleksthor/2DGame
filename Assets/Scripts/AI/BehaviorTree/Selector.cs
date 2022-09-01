using System.Collections.Generic;


namespace BehaviorTree
{
    public class Selector : Node
    {
        public Selector() : base() { }
        public Selector(List<Node> Children) : base(Children) { }

        public override NodeState Evaluate()
        {

            foreach (Node node in Children)
            {
                switch (node.Evaluate())
                {
                    case NodeState.NS_Failure:
                        continue;
                    case NodeState.NS_Success:
                        state = NodeState.NS_Success;
                        return state;
                    case NodeState.NS_Running:
                        state = NodeState.NS_Running;
                        return state;
                    default:
                        continue;
                }
            }

            state = NodeState.NS_Failure;
            return state;
        }
    }
}