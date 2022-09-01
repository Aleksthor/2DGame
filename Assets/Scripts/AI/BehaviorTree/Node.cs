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
        protected NodeState state;


        public Node Parent;

        // These children will be assigned in the constructor of the class, or else they’ll just be empty
        protected List<Node> Children = new List<Node>();

        private Dictionary<string, object> DataContext = new Dictionary<string, object>();

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


        public virtual NodeState Evaluate() => NodeState.NS_Failure;

        public void SetData(string key, object value)
        {
            DataContext[key] = value;
        }

        public object GetData(string key)
        {
            object value = null;
            if (DataContext.TryGetValue(key, out value))
                return value;

            Node node = Parent;
            while (node != null)
            {
                value = node.GetData(key);
                if (value != null)
                    return value;
                node = node.Parent;


            }
            return null;
        }

        public bool ClearData(string key)
        {
            if (DataContext.ContainsKey(key))
            {
                DataContext.Remove(key);
                return true;
            }

            Node node = Parent;
            while(node != null)
            {
                bool Cleared = node.ClearData(key);
                if (Cleared)
                    return true;
                node = node.Parent;
            }
            return false;
        }

    }

}

