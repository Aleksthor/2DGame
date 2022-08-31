using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BehaviorTree
{
    public enum NodeState
    {
        NS_Running,
        NS_Success,
        NS_Failure
    }


    public class Node
    {
        // State of Node
        protected NodeState State;


        public Node Parent;

        // These children will be assigned in the constructor of the class, or else they’ll just be empty
        protected List<Node> Children = new List<Node>();

        public Node()
        {
            Parent = null;
        }

        public Node(List<Node> Childen)
        {
            foreach(Node child in Children)
            {
                Attach(child);
            }
        }

        private void Attach(Node node)
        {
            node.Parent = this;
            Children.Add(node);
        }
    

    }

}

