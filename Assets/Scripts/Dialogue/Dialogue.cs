using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dialogue
{
    [CreateAssetMenu(fileName ="New Dialogue", menuName = "DialogueSystem/Dialogue", order = 0)]
    public class Dialogue : ScriptableObject
    {
        [SerializeField] List<DialogueNode> nodes = new List<DialogueNode>();

        Dictionary<string, DialogueNode> nodeLookup = new Dictionary<string, DialogueNode>();

        #region Awake / OnValidate
        // This will not be included in the final build
#if UNITY_EDITOR
        // Scriptable Object Awake, not MonoBehavior Awake
        private void Awake()
        {
            if (nodes.Count == 0)
            {
                DialogueNode rootNode = new DialogueNode();
                rootNode.uniqueID = Guid.NewGuid().ToString();
                nodes.Add(rootNode);
            }
            OnValidate();
        }
#endif

        // setup our dictionary for searching
        private void OnValidate()
        {
            nodeLookup.Clear();
            foreach (DialogueNode node in GetAllNodes())
            {
                nodeLookup[node.uniqueID] = node;
            }
        }
        #endregion

        // return all nodes in this object as IEnumerable. IEnumerable is any "object" as long as its in a List. (Not only List)
        public IEnumerable<DialogueNode> GetAllNodes()
        {
            return nodes;
        }

        // returns the current child in the foreach loop we are currently in in another script
        public IEnumerable<DialogueNode> GetAllChildren(DialogueNode node)
        {
            List<DialogueNode> result = new List<DialogueNode>();
            foreach(string childID in node.children)
            {
                if (nodeLookup.ContainsKey(childID))
                {
                    yield return nodeLookup[childID];
                }
            }
             
        }



        public void CreateNode(DialogueNode parent)
        {
            DialogueNode newNode = new DialogueNode();
            newNode.uniqueID = Guid.NewGuid().ToString();
            parent.children.Add(newNode.uniqueID);
            nodes.Add(newNode);
            OnValidate();
        }


        public void DeleteNode(DialogueNode nodeToDelete)
        {
            nodes.Remove(nodeToDelete);
            OnValidate();
            // delete children
            foreach(DialogueNode node in GetAllNodes())
            {
                node.children.Remove(nodeToDelete.uniqueID);
            }
        }
    }


}
