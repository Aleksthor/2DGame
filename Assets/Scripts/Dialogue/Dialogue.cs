using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Dialogue
{
    [CreateAssetMenu(fileName ="New Dialogue", menuName = "DialogueSystem/Dialogue", order = 0)]
    public class Dialogue : ScriptableObject, ISerializationCallbackReceiver
    {
        [SerializeField] List<DialogueNode> nodes = new List<DialogueNode>();

        Dictionary<string, DialogueNode> nodeLookup = new Dictionary<string, DialogueNode>();

        #region Awake / OnValidate
        // This will not be included in the final build

        // setup our dictionary for searching
        private void OnValidate()
        {
            nodeLookup.Clear();
            foreach (DialogueNode node in GetAllNodes())
            {
                nodeLookup[node.name] = node;
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
            foreach(string childID in node.GetChildren())
            {
                if (nodeLookup.ContainsKey(childID))
                {
                    yield return nodeLookup[childID];
                }
            }
             
        }



#if UNITY_EDITOR


        public void CreateNode(DialogueNode parent)
        {
            DialogueNode newNode = MakeNode(parent);

            Undo.RegisterCreatedObjectUndo(newNode, "Created Dialogue Node");
            Undo.RecordObject(this, "Added Dialogue Node");
            AddNode(newNode);
        }

        private DialogueNode MakeNode(DialogueNode parent)
        {
            DialogueNode newNode = CreateInstance<DialogueNode>();
            newNode.name = Guid.NewGuid().ToString();
            if (parent != null)
            {
                parent.AddChild(newNode.name);
                newNode.SetPlayerSpeaking(!parent.IsPlayerSpeaking());
                newNode.SetPosition(parent.GetRect().position + new Vector2(250, 0));
            }
            return newNode;
        }
        private void AddNode(DialogueNode newNode)
        {
            nodes.Add(newNode);

            OnValidate();
        }


        public void DeleteNode(DialogueNode nodeToDelete)
        {
            Undo.RecordObject(this, "Deleted Dialogue Node(s)");
            nodes.Remove(nodeToDelete);
            OnValidate();
            // delete children
            foreach(DialogueNode node in GetAllNodes())
            {
                node.RemoveChild(nodeToDelete.name);
            }
            Undo.DestroyObjectImmediate(nodeToDelete);
        }
#endif

        public void OnBeforeSerialize()
        {
#if UNITY_EDITOR
            if (nodes.Count == 0)
            {
                DialogueNode newNode = MakeNode(null);
                AddNode(newNode);
            }

            if (AssetDatabase.GetAssetPath(this) != "")
            {
                foreach(DialogueNode node in GetAllNodes())
                {
                    if (AssetDatabase.GetAssetPath(node) == "")
                    {
                        AssetDatabase.AddObjectToAsset(node, this);
                    }
                }
            }
#endif
        }

        public void OnAfterDeserialize()
        {
            
        }


        public DialogueNode GetRootNode()
        {
            if (nodes.Count > 0)
                return nodes[0];

            else
                return null;
        }
    }


}
