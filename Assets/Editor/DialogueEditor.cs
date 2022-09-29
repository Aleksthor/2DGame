using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;
using System;

namespace Dialogue.Editor
{
    
    // Class is of type EditorWindow
    public class DialogueEditor : EditorWindow
    {
        // the selected diaogue we ar currently editing
        Dialogue selectedDialogue = null;
        [NonSerialized] GUIStyle nodeStyle;
        [NonSerialized] DialogueNode selectedDialogueNode = null;
        [NonSerialized] Vector2 draggingOffset;
        [NonSerialized] DialogueNode creatingNode = null;
        [NonSerialized] DialogueNode deleteNode = null;
        [NonSerialized] DialogueNode linkingNode = null;
        Vector2 scrollPosition;
        [NonSerialized] bool draggingCanvas = false;
        [NonSerialized] Vector2 draggingCanvasOffset;

        #region Open Editor

        // How to create an option to open the window from the windows tab
        // callback by name
        [MenuItem("Window/DialogueEditor")]
        public static void ShowEditorWindow()
        {
            GetWindow(typeof(DialogueEditor), false, "Dialogue Editor");
        }


        // This gets called whenever we open ANY asset in the editor
        // callback by attribute
        [OnOpenAssetAttribute(1)]
        public static bool OnOpenAsset(int instanceID, int line)
        {
            // Cast using as. this will return null if the object type is not correct
            Dialogue dialogue = EditorUtility.InstanceIDToObject(instanceID) as Dialogue;

            if (dialogue == null)
            {
                return false;
            }
            else
            {
                // Same function that the Window tab uses
                ShowEditorWindow();
                return true;
            }
            
        }
        #endregion


        #region OnGUI / Update
        // Function is called whenever we interact with the editor in a "Update" kind of way
        // callback by name
        private void OnGUI()
        {
            // Print what dialogue object we have selected
            if (selectedDialogue == null)
            {
                EditorGUILayout.LabelField("No Dialogue Selected");
            }
            else
            {

                ProcessEvents();

                // Scrollbar
                scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);

                GUILayoutUtility.GetRect(4000, 4000);

                foreach (DialogueNode node in selectedDialogue.GetAllNodes())
                {                   
                    DrawConnections(node);
                }
                foreach (DialogueNode node in selectedDialogue.GetAllNodes())
                {
                    DrawNode(node);                    
                }

                EditorGUILayout.EndScrollView();

                if (creatingNode != null)
                {
                    Undo.RecordObject(selectedDialogue, "Created a new Dialogue Node");
                    selectedDialogue.CreateNode(creatingNode);
                    creatingNode = null;
                }
                if (deleteNode != null)
                {
                    Undo.RecordObject(selectedDialogue, "Deleted Dialogue Node(s)");
                    selectedDialogue.DeleteNode(deleteNode);
                    deleteNode = null;
                }

            }                       
        }

        #endregion


        #region Draw Bezier Curves
        private void DrawConnections(DialogueNode node)
        {
            Vector3 startPos = new Vector2(node.rect.xMax, node.rect.center.y);
            foreach (DialogueNode childNode in selectedDialogue.GetAllChildren(node))
            {
                
                Vector3 endPos = new Vector2(childNode.rect.xMin, childNode.rect.center.y);
                Vector3 controlPointOffset = endPos - startPos;
                controlPointOffset.y = 0f;
                controlPointOffset.x *= 0.8f;
                Handles.DrawBezier
                    (startPos, 
                    endPos, 
                    startPos + controlPointOffset, 
                    endPos - controlPointOffset, 
                    Color.black, null, 4f);
            }
        }
        #endregion


        #region Process Events (DragDrop)
        // Drag Drop Functionability
        private void ProcessEvents()
        {
            if (Event.current.type == EventType.MouseDown && selectedDialogueNode == null)
            {
                selectedDialogueNode = GetNodeAtPoint(Event.current.mousePosition + scrollPosition);
                if (selectedDialogueNode != null)
                {
                    draggingOffset = selectedDialogueNode.rect.position - Event.current.mousePosition;
                }
                else
                {
                    draggingCanvas = true;
                    draggingCanvasOffset = Event.current.mousePosition + scrollPosition;
                }               
            }
            else if (Event.current.type == EventType.MouseDrag && selectedDialogueNode != null)
            {
                Undo.RecordObject(selectedDialogue, "Move Dialogue Node");
                // Set Position
                if (selectedDialogueNode != null)
                {
                    selectedDialogueNode.rect.position = Event.current.mousePosition + draggingOffset;
                }
               
                GUI.changed = true;
            }
            else if (Event.current.type == EventType.MouseDrag && draggingCanvas == true)
            {
                scrollPosition = draggingCanvasOffset - Event.current.mousePosition;
                GUI.changed = true;
            }
            else if (Event.current.type == EventType.MouseUp && selectedDialogueNode != null)
            {
                selectedDialogueNode = null;

            }
            else if (Event.current.type == EventType.MouseUp && draggingCanvas == true)
            {
                draggingCanvas = false;

            }


        }
        #endregion


        #region Draw Each Node
        private void DrawNode(DialogueNode node)
        {
            GUILayout.BeginArea(node.rect, nodeStyle);
            // this will check if any value change unity end check is called
            EditorGUI.BeginChangeCheck();


            string newText = EditorGUILayout.TextField(node.text);

            GUILayout.BeginHorizontal();
            // EndChangeCheck will return a bool
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(selectedDialogue, "Update Dialoge Text");

                node.text = newText;

            }


            if (GUILayout.Button("delete"))
            {
                deleteNode = node;
            }

            LinkButton(node);

            if (GUILayout.Button("create"))
            {
                creatingNode = node;
            }
            GUILayout.EndHorizontal();

            GUILayout.EndArea();
        }

        private void LinkButton(DialogueNode node)
        {
            if (linkingNode == null)
            {
                if (GUILayout.Button("link"))
                {
                    linkingNode = node;
                }
            }
            else if (linkingNode == node)
            {
                if (GUILayout.Button("cancel"))
                {
                    linkingNode = null;
                }
            }
            else if (linkingNode.children.Contains(node.uniqueID))
            {
                if (GUILayout.Button("unlink"))
                {
                    Undo.RecordObject(selectedDialogue, "Remove Dialogue Link");
                    linkingNode.children.Remove(node.uniqueID);
                    linkingNode = null;
                }
            }
            else
            {

                if (GUILayout.Button("child"))
                {
                    Undo.RecordObject(selectedDialogue, "Add Dialogue Link");
                    linkingNode.children.Add(node.uniqueID);
                    linkingNode = null;
                }
            }
            
            
        }
        #endregion


        #region OnEnable / Awake
        // callback by name
        private void OnEnable()
        {
            Selection.selectionChanged += SelectionChanged;

            // Setup The NodeStyle
            nodeStyle = new GUIStyle();
            nodeStyle.normal.background = EditorGUIUtility.Load("node0") as Texture2D;
            nodeStyle.padding = new RectOffset(20, 20, 20, 20);
            nodeStyle.border = new RectOffset(12,12,12,12); 
        }
        #endregion


        #region Event SelectionChanged
        // callback by event
        private void SelectionChanged()
        {
            Dialogue dialogue = Selection.activeObject as Dialogue;
            if (dialogue != null)
            {
                selectedDialogue = dialogue;
                Repaint();
            }
        }
        #endregion


        #region GetNodeAtPoint (DragDrop)
        // used for drag dropping
        private DialogueNode GetNodeAtPoint(Vector2 mousePos)
        {
            DialogueNode returnNode = null;
            foreach(DialogueNode dialogeNode in selectedDialogue.GetAllNodes())
            {
                if(dialogeNode.rect.Contains(mousePos))
                {
                    returnNode = dialogeNode;
                }
            }
            return returnNode;
        }
        #endregion

    }

}
