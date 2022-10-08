using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dialogue
{

    public class AIConversant : MonoBehaviour
    {
        [SerializeField] string conversantName;
        private GameObject playerObject;
        private PlayerConversant playerConversant;
        public float interactRange = 3f;

        [SerializeField] Dialogue AIDialogue;

        private void Start()
        {
            playerObject = PlayerSingleton.instance.gameObject;
            playerConversant = PlayerSingleton.instance.GetComponent<PlayerConversant>();
        }
        private void Update()
        {
            if (Vector2.Distance(playerObject.transform.position, transform.position) < interactRange && !playerConversant.isActive())
            {

                if (Input.GetButton("Use"))
                {
                    playerConversant.StartDialogue(this, AIDialogue);
                }
                

            }

            if (Vector2.Distance(playerObject.transform.position, transform.position) > interactRange 
                && playerConversant.GetCurrentDialogue() == AIDialogue &&  AIDialogue != null) 
            {
                playerConversant.Quit();
            }


        }
        public string GetConversantName()
        {
            return conversantName;
        }
    }
}