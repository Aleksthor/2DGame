using System.Collections.Generic;


namespace BehaviorTree
{
    public class Sequence : Node
    {
        public Sequence() : base() { }
        public Sequence(List<Node> Children) : base(Children) { }

        public override NodeState Evaluate()
        {
            bool AnyChildIsRunning = false;

            foreach (Node node in Children)
            {
                switch (node.Evaluate())
                {
                    case NodeState.NS_Failure:
                        state = NodeState.NS_Failure;
                        return state;
                    case NodeState.NS_Success:
                        continue;
                    case NodeState.NS_Running:
                        AnyChildIsRunning = true;
                        continue;
                    default:
                        state = NodeState.NS_Success;
                        return state;
                }
            }

            state = AnyChildIsRunning ? NodeState.NS_Running : NodeState.NS_Success;
            return state;
        }
    }
}

