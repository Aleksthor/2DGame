using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace Dialogue
{
    public class PlayerConversant : MonoBehaviour
    {
        [SerializeField] Dialogue currentDialogue;
        [SerializeField] DialogueNode currentNode;

        private void Awake()
        {
            if (currentDialogue != null)
                currentNode = currentDialogue.GetRootNode();    
        }

        public string GetText()
        {
            if (currentNode == null)
            {
                return "";
            }
            else
            {
                return currentNode.GetText();
            }
        }

        public void Next()
        {
            DialogueNode[] children = currentDialogue.GetAllChildren(currentNode).ToArray();
            currentNode =  children[0];
        }


        public bool HasNext()
        {
            if (currentDialogue.GetAllChildren(currentNode).Count() > 0)
            {
                return true;
            }
            return false;
        }
    }

}
