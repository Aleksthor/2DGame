using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

namespace Dialogue
{

    public class DialogueNode : ScriptableObject
    {
        [SerializeField] private string text;
        [SerializeField] private List<string> children = new List<string>();
        [SerializeField] private Rect rect = new Rect(0,0,200,100);
        [SerializeField] private bool isPlayerSpeaking = false;
        [SerializeField] private string onEnterAction;
        [SerializeField] private string onExitAction;
        public Rect GetRect()
        {
            return rect; 
        }

        public string GetText()
        {
            return text;
        }

        public List<string> GetChildren()
        {
            return children;
        }   
        
        public bool IsPlayerSpeaking()
        {
            return isPlayerSpeaking;
        }

#if UNITY_EDITOR
        public void SetPosition(Vector2 input)
        {
            Undo.RecordObject(this, "Move Dialogue Node");
            rect.position = input;
            EditorUtility.SetDirty(this);
        }


        public void SetText(string input)
        {
            if (text != input)
            {
                Undo.RecordObject(this, "Update Dialoge Text");
                text = input;
                EditorUtility.SetDirty(this);
            }
        }

        public void AddChild(string childID)
        {
            Undo.RecordObject(this, "Add Dialogue Link");
            children.Add(childID);
            EditorUtility.SetDirty(this);
        }

        public void RemoveChild(string childID)
        {
            Undo.RecordObject(this, "Remove Dialogue Link");
            children.Remove(childID);
            EditorUtility.SetDirty(this);
        }

        public void SetPlayerSpeaking(bool IsPlayerSpeaking)
        {
            Undo.RecordObject(this, "Change Dialogue Speaker");
            isPlayerSpeaking = IsPlayerSpeaking;
            EditorUtility.SetDirty(this);
        }


        public string GetOnEnterAction()
        {
            return onEnterAction;
        }

        public string GetOnExitAction()
        {
            return onExitAction;
        }
#endif
    }
}

