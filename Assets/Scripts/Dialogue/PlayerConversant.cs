using System;
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
        AIConversant currentConversant = null;
        [SerializeField] DialogueNode currentNode;

        

        bool isChoosing = false;

        private void Awake()
        {
            if (currentDialogue != null)
                currentNode = currentDialogue.GetRootNode();    
        }

        public void StartDialogue(AIConversant newConversant,Dialogue newDialogue)
        {
            currentDialogue = newDialogue;
            currentConversant = newConversant;
            currentNode = currentDialogue.GetRootNode();
            TriggerEnterAction();
            GameEvents.current.ConversationUpdated();
        }

        public bool isActive()
        {
            return currentDialogue != null;
        }

        public void Quit()
        {
            TriggerExitAction();
            currentDialogue = null;
            currentNode = null;
            isChoosing = false;
            currentConversant = null;
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

        public string GetCurrentConversantName()
        {
            if (currentConversant != null)
            {
                return currentConversant.GetConversantName();
            }
            else
            {
                return "";
            }
        }

        public void Next()
        {
            int numberOfPlayerResponses = currentDialogue.GetPlayerChildren(currentNode).Count();

            if (numberOfPlayerResponses > 0)
            {
                isChoosing = true;
                TriggerExitAction();
            }
            else
            {
                DialogueNode[] children = currentDialogue.GetAIChildren(currentNode).ToArray();
                TriggerExitAction();
                currentNode = children[0];
                TriggerExitAction();
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
            TriggerEnterAction();
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

        private void TriggerEnterAction()
        {
            if (currentNode != null)
            {
                TriggerAction(currentNode.GetOnEnterAction());
            }
        }

        private void TriggerExitAction()
        {

            if (currentNode != null)
            {
                TriggerAction(currentNode.GetOnExitAction());
            }   
        }

        private void TriggerAction(string action)
        {
            if (currentNode.GetOnEnterAction() != "")
            {
                return;
            }


            foreach(DialogueTrigger trigger in currentConversant.GetComponents<DialogueTrigger>())
            {
                trigger.Trigger(action);
            }
        }
    }

}
