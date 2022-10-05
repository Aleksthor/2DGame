using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Dialogue
{
    public class PlayerConversant : MonoBehaviour
    {
        Dialogue currentDialogue;
        [SerializeField] DialogueNode currentNode;

        

        bool isChoosing = false;

        private void Awake()
        {
            if (currentDialogue != null)
                currentNode = currentDialogue.GetRootNode();    
        }

        public void StartDialogue(Dialogue newDialogue)
        {
            currentDialogue = newDialogue;

            currentNode = currentDialogue.GetRootNode();

            GameEvents.current.ConversationUpdated();
        }

        public bool isActive()
        {
            return currentDialogue != null;
        }

        public void Quit()
        {
            currentDialogue = null;
            currentNode = null;
            isChoosing = false;
            GameEvents.current.ConversationUpdated();
        }


        public bool IsChoosing()
        {
            return isChoosing;
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
            int numberOfPlayerResponses = currentDialogue.GetPlayerChildren(currentNode).Count();

            if (numberOfPlayerResponses > 0)
            {
                isChoosing = true;
            }
            else
            {
                DialogueNode[] children = currentDialogue.GetAIChildren(currentNode).ToArray();
                currentNode = children[0];
            }
            GameEvents.current.ConversationUpdated();
        }


        public IEnumerable<DialogueNode> GetChoices()
        {
            return currentDialogue.GetPlayerChildren(currentNode);
        }


        public void SelectChoice(DialogueNode chosenNode)
        {
            currentNode = chosenNode;
            isChoosing = false;
            Next();
        }


        public bool HasNext()
        {
            if (currentDialogue.GetAllChildren(currentNode).Count() > 0)
            {
                return true;
            }
            return false;
        }

        public Dialogue GetCurrentDialogue()
        {
            return currentDialogue;
        }
    }

}
